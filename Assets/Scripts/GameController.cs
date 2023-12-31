using System;
using System.Linq;
using Core;
using Core.GameWorld;
using Core.GameWorld.Entities.Asteroid;
using Core.GameWorld.Entities.EnemyShip;
using Core.GameWorld.Entities.PlayerShip;
using Core.GameWorld.Entities.Projectile.Bullet;
using Core.GameWorld.Entities.Projectile.Laser;
using Core.GameWorld.EntitiesSpawner;
using Core.GameWorld.SpawnLocation;
using Core.Input;
using Core.Tools.Extensions;
using Core.Tools.InfinityWorld;
using Core.Tools.ServiceLocator;
using Core.UI;
using GameWorld;
using GameWorld.Entities.Asteroid;
using GameWorld.Entities.EnemyShip;
using GameWorld.Entities.PlayerShip;
using GameWorld.Entities.PlayerShip.Projectile.Bullet;
using GameWorld.Entities.PlayerShip.Projectile.Laser;
using Input;
using UI;
using UnityEngine;
using Object = UnityEngine.Object;
using PlayerInput = Core.Input.PlayerInput;

public class GameController : IDisposable {
	public event Action OnRestart;
	
	private readonly Config config;

	private readonly InputController inputController;
	private readonly IPlayerShipController playerShipController;
	private readonly PlayerShipInputController playerShipInputController;

	private readonly WorldObjectsContainer worldObjectsContainer = new WorldObjectsContainer();

	private readonly AsteroidFactory bigAsteroidFactory;
	private readonly AsteroidFactory smallAsteroidFactory;
	private readonly BulletFactory bulletFactory;
	private readonly LaserFactory laserFactory;

	private readonly IEntitiesSpawner[] spawners;

	private readonly Scorer scorer;
	private readonly UIController uiController;

	public GameController(Config config){
		this.config = config;

		ServiceLocator.Bind(new InputActions()).AsSingle();
		ServiceLocator.Bind(new PlayerInput()).AsSingle();
		ServiceLocator.Bind(new WorldBoundsProvider(Camera.main)).AsSingle();

		var cameraOrthographicBounds = Camera.main.OrthographicBounds();
		Bounds worldBounds = new Bounds((Vector2) cameraOrthographicBounds.center,
										(Vector2) cameraOrthographicBounds.size);
		ServiceLocator.Bind(new InfinityWorld(worldBounds)).AsSingle();

		inputController = new InputController();

		bulletFactory = new BulletFactory(
			new PlayerBulletPool(
				config.Bullet,
				config.BulletsPoolParent,
				PlayerBulletPool.PoolType.ObjectPool,
				20,
				true),
			config.BulletConfig);

		ServiceLocator.Bind<IBulletFactory>(bulletFactory).AsSingle();

		laserFactory = new LaserFactory(
			new LaserPool(
				config.Laser,
				config.BulletsPoolParent,
				LaserPool.PoolType.ObjectPool,
				10,
				true),
			config.LaserConfig);

		ServiceLocator.Bind<ILaserFactory>(laserFactory).AsSingle();

		worldObjectsContainer.RegisterWorldObjectsFactory(bulletFactory);
		worldObjectsContainer.RegisterWorldObjectsFactory(laserFactory);

		playerShipController = new PlayerShipController(
			Object.Instantiate(config.PlayerShip),
			config.PlayerShipConfig){
			Position = Vector2.zero,
			Forward = Vector2.up
		};

		ServiceLocator.Bind(new PlayerShipProvider(playerShipController)).AsSingle();

		playerShipInputController = new PlayerShipInputController(playerShipController);
		
		scorer = new Scorer();
		ServiceLocator.Bind(scorer).AsSingle();
		
		var enemyShipFactory = new EnemyShipFactory(
			new EnemyShipPool(
				config.EnemyShip,
				config.EnemyShipsPoolParent,
				EnemyShipPool.PoolType.ObjectPool,
				10,
				true),
			config.EnemyShipConfig);

		ServiceLocator.Bind(enemyShipFactory).AsSingle();

		worldObjectsContainer.RegisterWorldObjectsFactory(enemyShipFactory);

		smallAsteroidFactory = new AsteroidFactory(
			new AsteroidPool(
				config.SmallAsteroid,
				config.AsteroidsPoolParent,
				AsteroidPool.PoolType.ObjectPool,
				30,
				true),
			config.SmallAsteroidConfig);

		ServiceLocator.Bind<IAsteroidFactory>(smallAsteroidFactory)
					  .WithTag("SmallAsteroidsFactory")
					  .AsSingle();

		worldObjectsContainer.RegisterWorldObjectsFactory(smallAsteroidFactory);

		config.BigAsteroidConfig.AsteroidFactory = smallAsteroidFactory;
		bigAsteroidFactory = new AsteroidFactory(
			new AsteroidPool(
				config.BigAsteroid,
				config.AsteroidsPoolParent,
				AsteroidPool.PoolType.ObjectPool,
				10,
				true),
			config.BigAsteroidConfig);

		ServiceLocator.Bind<IAsteroidFactory>(bigAsteroidFactory)
					  .WithTag("BigAsteroidsFactory")
					  .AsSingle();

		worldObjectsContainer.RegisterWorldObjectsFactory(bigAsteroidFactory);

		WorldBoundsSpawnLocation worldBoundsSpawnLocation = new WorldBoundsSpawnLocation(2.0f);

		spawners = new IEntitiesSpawner[]{
			new EnemyShipPeriodicalSpawner(
				worldBoundsSpawnLocation,
				enemyShipFactory,
				0.5f
			),
			new AsteroidPeriodicalSpawner(
				worldBoundsSpawnLocation,
				bigAsteroidFactory,
				0.5f,
				config.BigAsteroidConfig.MovementMaxSpeed,
				200.0f),
			new AsteroidPeriodicalSpawner(
				worldBoundsSpawnLocation,
				smallAsteroidFactory,
				0.25f,
				config.BigAsteroidConfig.MovementMaxSpeed * 2.0f,
				200.0f * 2.0f),
		};

		uiController = new UIController(config.UIView);

		uiController.onRestartPressed += Restart;

		playerShipController.OnDispose += RunGameOver;
	}

	private void Restart(){
		OnRestart?.Invoke();
	}

	private void RunGameOver(IWorldObjectController obj){
		Time.timeScale = 0.0f;
		uiController.ShowGameOver();
	}

	public void UpdateGameplay(float dt){
		inputController.Update();

		playerShipInputController.Update(dt);

		playerShipController.Update(dt);

		var x = worldObjectsContainer.WorldObjectControllers.ToList();
		foreach (IWorldObjectController worldObjectController in x){
			worldObjectController.Update(dt);
		}

		foreach (IEntitiesSpawner spawner in spawners){
			spawner.Update(dt);
		}
	}

	public void UpdateUI(){
		uiController.SetStats(
			new GameplayStats(
				playerShipController.Position,
				playerShipController.Forward.PosNegAngle(),
				playerShipController.Speed.magnitude,
				playerShipController.LaserCharges,
				playerShipController.LaserChargeTimeLeft,
				scorer.CurrentScore
			));
	}

	public void Dispose(){
		inputController.Dispose();

		worldObjectsContainer.Dispose();

		uiController.onRestartPressed -= Restart;

		playerShipController.OnDispose -= RunGameOver;
		
		scorer.Dispose();

		ServiceLocator.UnbindAll();
	}

	[Serializable]
	public class Config {
		[SerializeField] private UIView uiView;
		[SerializeField] private PlayerShip playerShip;
		[SerializeField] private EnemyShip enemyShip;
		[SerializeField] private Transform enemyShipsPoolParent;
		[SerializeField] private Asteroid bigAsteroid;
		[SerializeField] private Asteroid smallAsteroid;
		[SerializeField] private Transform asteroidsPoolParent;
		[SerializeField] private Bullet bullet;
		[SerializeField] private Laser laser;
		[SerializeField] private Transform bulletsPoolParent;
		[SerializeField] private PlayerShipConfig playerShipConfig;
		[SerializeField] private EnemyShipConfig enemyShipConfig;
		[SerializeField] private BulletConfig bulletConfig;
		[SerializeField] private LaserConfig laserConfig;
		[SerializeField] private AsteroidConfig bigAsteroidConfig;
		[SerializeField] private AsteroidConfig smallAsteroidConfig;

		public UIView UIView => uiView;
		public PlayerShip PlayerShip => playerShip;
		public EnemyShip EnemyShip => enemyShip;
		public Transform EnemyShipsPoolParent => enemyShipsPoolParent;
		public Asteroid BigAsteroid => bigAsteroid;
		public Asteroid SmallAsteroid => smallAsteroid;
		public Transform AsteroidsPoolParent => asteroidsPoolParent;
		public Bullet Bullet => bullet;
		public Laser Laser => laser;
		public Transform BulletsPoolParent => bulletsPoolParent;
		public PlayerShipConfig PlayerShipConfig => playerShipConfig;
		public EnemyShipConfig EnemyShipConfig => enemyShipConfig;
		public BulletConfig BulletConfig => bulletConfig;
		public LaserConfig LaserConfig => laserConfig;
		public AsteroidConfig BigAsteroidConfig => bigAsteroidConfig;
		public AsteroidConfig SmallAsteroidConfig => smallAsteroidConfig;
	}
}