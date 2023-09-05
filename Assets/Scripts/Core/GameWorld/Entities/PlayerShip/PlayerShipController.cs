using System;
using Core.GameWorld.Entities.Bullet.PlayerBullet;
using Core.GameWorld.Entities.Projectile.Bullet;
using Core.GameWorld.MovementController;
using Core.GameWorld.ShootController;
using Core.Tools.ServiceLocator;

namespace Core.GameWorld.Entities.PlayerShip {
//	public class EventASD {
//		public event Action Event;
//
////		public void Invoke() => Event?.Invoke();
//	}

	public class PlayerShipController : BaseWorldObjectController<IPlayerShipView>, IPlayerShipController {
		private readonly PlayerShipConfig shipConfig;

		private readonly PhysicMovementController movementController;

		private readonly IShootController gun1ShootController;
		private readonly IShootController gun2ShootController;

		private readonly PlayerGunConfig gun1Config;
		private readonly PlayerGunConfig gun2Config;


//		private EventASD OnStopGun2Shooting = new EventASD();


		public PlayerShipController(IPlayerShipView view, PlayerShipConfig shipConfig) : base(view){
			this.shipConfig = shipConfig;

			movementController = new PhysicMovementController(
				this,
				new PhysicMovementController.Config(
					shipConfig.MovementDumping,
					shipConfig.RotationDumping));


//			var gun1Config = ServiceLocator.Resolve<PlayerGunConfig>("PlayerGun1Config");
//			var gun2Config = ServiceLocator.Resolve<PlayerGunConfig>("PlayerGun2Config");

			var bulletFactory = ServiceLocator.Resolve<IBulletFactory>();
//			var gun1ProjectileFactory = gun1Config.projectileFactory;
//			var gun1ProjectileFactory = ServiceLocator.Resolve<IProjectileFactory
//				//<SimpleBulletController, BulletFactoryArgs>
//			>("PlayerGun1ProjectileFactory");

			gun1ShootController = new ShootController.ShootController //<SimpleBulletController, BulletFactoryArgs>
			(
				this,
				bulletFactory,
//				gun1ProjectileFactory,
//				gun1Config.projectileFactoryArgs,
				() => new BulletFactoryArgs(Position, Forward, movementController.Speed, movementController.AngularSpeed),
				shipConfig.Gun1FireRate,
				false
			);

			var laserFactory = ServiceLocator.Resolve<ILaserFactory>();
//			var gun2ProjectileFactory = gun2Config.projectileFactory;
//			var gun2ProjectileFactory = ServiceLocator.Resolve<IProjectileFactory
//				//<LaserController, LaserFactoryArgs>
//			>("PlayerGun2ProjectileFactory");

			gun2ShootController = new ChargableShootController //<LaserController, LaserFactoryArgs>
			(
				this,
				laserFactory,
//				gun2ProjectileFactory,
//				gun2Config.projectileFactoryArgs,
				() => new LaserFactoryArgs(Position, Forward, this),
				shipConfig.Gun2FireRate,
				true,
				shipConfig.Gun2MaxCharges,
				shipConfig.Gun2ChargeTime
			);


//			gun1ShootController = ServiceLocator.Resolve<IShootController>("PlayerGun1ShootController");
//			gun2ShootController = ServiceLocator.Resolve<IShootController>("PlayerGun2ShootController");
		}

		public void SetForwardSpeedThrottle(float value) =>
			movementController.SetForwardMovingForce(value * shipConfig.ForwardMovingMaxForce);

		public void SetAngularSpeedThrottle(float value){
			//			movementController.AddAngularSpeed(value * shipConfig.RotationMaxForce);
			movementController.SetAngularSpeed(value * shipConfig.RotationSpeed);
		}

		public bool Gun1IsSpawnActive{
			get => gun1ShootController.IsSpawnActive;
			set => gun1ShootController.IsSpawnActive = value;
		}

		public bool Gun2IsSpawnActive{
			get => gun2ShootController.IsSpawnActive;
			set => gun2ShootController.IsSpawnActive = value;
		}

		public override void Update(float dt){
			movementController.Update(dt);

			gun1ShootController.Update(dt);
			gun2ShootController.Update(dt);

			base.Update(dt);
		}

		public override void Dispose(){
//			OnStopGun2Shooting.Invoke();

			base.Dispose();
		}
	}
}