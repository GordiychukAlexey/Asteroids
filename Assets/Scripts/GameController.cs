using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.GameWorld;
using Core.GameWorld.Entities.Asteroid;
using Core.GameWorld.Entities.Bullet;
using Core.GameWorld.Entities.Bullet.PlayerBullet;
using Core.GameWorld.Entities.EnemyShip;
using Core.GameWorld.Entities.Laser;
using Core.GameWorld.Entities.PlayerShip;
using Core.GameWorld.Entities.Projectile;
using Core.GameWorld.Entities.Projectile.Bullet;
using Core.GameWorld.EntitiesSpawner;
using Core.GameWorld.SpawnLocation;
using Core.Input;
using Core.Tools;
using Core.Tools.Extensions;
using Core.Tools.InfinityWorld;
using Core.Tools.ServiceLocator;
using GameWorld.Entities.Asteroid;
using GameWorld.Entities.EnemyShip;
using GameWorld.Entities.PlayerShip;
using GameWorld.Entities.PlayerShip.Projectile.Bullet;
using GameWorld.Entities.PlayerShip.Projectile.Laser;
using Input;
using UnityEngine;
using Object = UnityEngine.Object;
using PlayerInput = Core.Input.PlayerInput;
using Random = UnityEngine.Random;

public class GameController : IDisposable {
	private readonly Config config;

	private readonly InputController inputController;
	private readonly IPlayerShipController playerShipController;

	private readonly PlayerShipInputController playerShipInputController;
//	private readonly List<IEnemyShipController> enemyShipControllers = new List<IEnemyShipController>(); //todo List?
//	private readonly List<IBulletController> bulletControllers = new List<IBulletController>();          //todo List?
//	private readonly List<IAsteroidController> asteroidControllers = new List<IAsteroidController>();    //todo List?
//	
//	private readonly List<IWorldObjectController> worldObjectControllers = new List<IWorldObjectController>();    //todo List?

	private readonly WorldObjectsContainer worldObjectsContainer = new WorldObjectsContainer();

	private readonly AsteroidFactory bigAsteroidFactory;
	private readonly AsteroidFactory smallAsteroidFactory;
	private readonly BulletFactory bulletFactory;
	private readonly LaserFactory laserFactory;

	private readonly IEntitiesSpawner[] spawners;

	public GameController(Config config){
		this.config = config;

		ServiceLocator.Bind(new InputActions()).AsSingle();
		ServiceLocator.Bind(new PlayerInput()).AsSingle();
		ServiceLocator.Bind(new WorldBoundsProvider(Camera.main)).AsSingle();

		var cameraOrthographicBounds = Camera.main.OrthographicBounds();
		Bounds worldBounds = new Bounds((Vector2) cameraOrthographicBounds.center,
										(Vector2) cameraOrthographicBounds.size);
		ServiceLocator.Bind(new InfinityWorld(worldBounds)).AsSingle();

//		ServiceLocator.Bind(new InfinityWorldPositionAdapter()).AsSingle();
//		ServiceLocator.Bind(new PositionToVirtualPositionsAdapter()).AsSingle();
//		ServiceLocator.Bind(new InfinityWorldPositionShiftProvider()).AsSingle();

		inputController = new InputController();

		bulletFactory = new BulletFactory(
			new PlayerBulletPool(
				config.PlayerGun1Bullet,
				config.BulletsPoolParent,
				PlayerBulletPool.PoolType.ObjectPool,
				20,
				true),
			config.BulletConfig);

		ServiceLocator.Bind<IBulletFactory
						   //<SimpleBulletController, BulletFactoryArgs>
					   >(bulletFactory)
//					  .WithTag("PlayerGun1ProjectileFactory")
					  .AsSingle();

		laserFactory = new LaserFactory(
			new LaserPool(
				config.Laser,
				config.BulletsPoolParent,
				LaserPool.PoolType.ObjectPool,
				10,
				true),
			config.LaserConfig);

		ServiceLocator.Bind<ILaserFactory
						   //<LaserController, LaserFactoryArgs>
					   >(laserFactory)
//					  .WithTag("PlayerGun2ProjectileFactory")
					  .AsSingle();

		worldObjectsContainer.RegisterWorldObjectsFactory(bulletFactory);
		worldObjectsContainer.RegisterWorldObjectsFactory(laserFactory);

//		playerGun1BulletFactory.OnCreateEvent += PlayerGunBulletFactoryOnCreateEventHandler;
//		playerGun2BulletFactory.OnCreateEvent += PlayerGunBulletFactoryOnCreateEventHandler;

//		playerGun1BulletFactory.OnCreateEvent += RegisterWorldObject;
//		playerGun2BulletFactory.OnCreateEvent += RegisterWorldObject;

		PlayerShipFactory playerShipFactory = new PlayerShipFactory(config.PlayerShip);
		playerShipController = playerShipFactory.Create(
			new PlayerShipFactory.Args(
				Vector2.zero, Vector2.up, config.PlayerShipConfig));
//		playerShipController = new PlayerShipController(
//			Object.Instantiate(config.PlayerShip), config.PlayerShipConfig){
//			Position = Vector2.zero,
//			Forward = Vector2.up
//		};

		ServiceLocator.Bind(new PlayerShipProvider(playerShipController)).AsSingle();

		playerShipInputController = new PlayerShipInputController(playerShipController);


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

//		bigAsteroidFactory.OnCreateEvent += AsteroidFactoryOnCreateEventHandler;
//		bigAsteroidFactory.OnCreateEvent += RegisterWorldObject;

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
				config.BigAsteroidConfig.MovementMaxSpeed*2.0f,
				200.0f*2.0f), 
		};

//		smallAsteroidFactory.OnCreateEvent += AsteroidFactoryOnCreateEventHandler;
//		smallAsteroidFactory.OnCreateEvent += RegisterWorldObject;

		CreateEnemy(Vector2.one * 18, Vector2.up); //testdel
	}

//	private void PlayerGunBulletFactoryOnCreateEventHandler(IBulletController bulletController){
//		bulletController.OnDispose += OnDisposeBullet;
//
//		bulletControllers.Add(bulletController);
//	}
//
//	private void AsteroidFactoryOnCreateEventHandler(IAsteroidController asteroidController){
//		asteroidController.OnDispose += OnDisposeAsteroid;
//
//		asteroidControllers.Add(asteroidController);
//	}

//	private void RegisterWorldObject(IWorldObjectController worldObjectController){
//		worldObjectController.OnDispose += OnDisposeWorldObject;
//		worldObjectControllers.Add(worldObjectController);
//	}
//	
//	private void OnDisposeWorldObject(IWorldObjectController controller){
//		controller.OnDispose -= OnDisposeAsteroid;
//		worldObjectControllers.Remove(controller);
//	}

	public void Update(float dt){
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

//		for (var i = 0; i < asteroidControllers.Count; i++){
//			asteroidControllers[i].Update(dt);
//		}
//
//		for (var i = 0; i < enemyShipControllers.Count; i++){
//			enemyShipControllers[i].Update(dt);
//		}
//
//		for (var i = 0; i < bulletControllers.Count; i++){
//			bulletControllers[i].Update(dt);
//		}
	}

	public void CreateEnemy(Vector2 position, Vector2 forward){
		EnemyShipFactory enemyShipFactory = ServiceLocator.Resolve<EnemyShipFactory>();

		var enemyShipController = enemyShipFactory.Create(
			new EnemyShipFactoryArgs(
				position,
				forward));

//		enemyShipController.OnDispose += OnDisposeEnemyShip;

//		enemyShipControllers.Add(enemyShipController);

		ServiceLocator.Resolve<AsteroidFactory>("SmallAsteroidsFactory")
					  .Create(new AsteroidFactoryArgs(
								  Vector2.right * (Random.value * 8.0f + 2.0f),
								  Vector2.up,
								  Vector2.one * 1.5f,
								  30.0f));
	}

//	private void OnDisposeEnemyShip(EnemyShipController enemyShipController){
//		enemyShipController.OnDispose -= OnDisposeEnemyShip;
//		enemyShipControllers.Remove(enemyShipController);
//	}

//	private void OnDisposeBullet(IWorldObjectController controller){
//		controller.OnDispose -= OnDisposeBullet;
//		bulletControllers.Remove((IBulletController) controller);
//	}
//
//	private void OnDisposeAsteroid(IWorldObjectController controller){
//		controller.OnDispose -= OnDisposeAsteroid;
//		asteroidControllers.Remove((IAsteroidController) controller);
//	}

	public void Dispose(){
		inputController.Dispose();

//		playerGun1BulletFactory.OnCreateEvent -= PlayerGunBulletFactoryOnCreateEventHandler;
//		playerGun2BulletFactory.OnCreateEvent -= PlayerGunBulletFactoryOnCreateEventHandler;


		worldObjectsContainer.Dispose();

//		bigAsteroidFactory.OnCreateEvent -= RegisterWorldObject;
//		smallAsteroidFactory.OnCreateEvent -= RegisterWorldObject;
//		playerGun1BulletFactory.OnCreateEvent -= RegisterWorldObject;
//		playerGun2BulletFactory.OnCreateEvent -= RegisterWorldObject;
	}

	[Serializable]
	public class Config {
		[SerializeField] private PlayerShip playerShip;
		[SerializeField] private EnemyShip enemyShip;
		[SerializeField] private Transform enemyShipsPoolParent;
		[SerializeField] private Asteroid bigAsteroid;
		[SerializeField] private Asteroid smallAsteroid;
		[SerializeField] private Transform asteroidsPoolParent;
		[SerializeField] private Bullet playerGun1Bullet;
		[SerializeField] private Bullet playerGun2Bullet;
		[SerializeField] private Laser laser;
		[SerializeField] private Transform bulletsPoolParent;
		[SerializeField] private PlayerShipConfig playerShipConfig;
		[SerializeField] private EnemyShipConfig enemyShipConfig;
		[SerializeField] private BulletConfig bulletConfig;
		[SerializeField] private LaserConfig laserConfig;
		[SerializeField] private AsteroidConfig bigAsteroidConfig;
		[SerializeField] private AsteroidConfig smallAsteroidConfig;

		public PlayerShip PlayerShip => playerShip;
		public EnemyShip EnemyShip => enemyShip;
		public Transform EnemyShipsPoolParent => enemyShipsPoolParent;
		public Asteroid BigAsteroid => bigAsteroid;
		public Asteroid SmallAsteroid => smallAsteroid;
		public Transform AsteroidsPoolParent => asteroidsPoolParent;
		public Bullet PlayerGun1Bullet => playerGun1Bullet;
		public Bullet PlayerGun2Bullet => playerGun2Bullet;
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