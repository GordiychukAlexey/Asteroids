using Core.GameWorld.Entities.Projectile.Bullet;
using Core.GameWorld.Entities.Projectile.Laser;
using Core.GameWorld.MovementController;
using Core.GameWorld.ShootController;
using Core.Tools.ServiceLocator;

namespace Core.GameWorld.Entities.PlayerShip {
	public class PlayerShipController : BaseWorldObjectController<IPlayerShipView>, IPlayerShipController {
		private readonly PlayerShipConfig shipConfig;

		private readonly PhysicMovementController movementController;

		private readonly IShootController gun1ShootController;
		private readonly IShootController gun2ShootController;

		private readonly PlayerGunConfig gun1Config;
		private readonly PlayerGunConfig gun2Config;

		public PlayerShipController(IPlayerShipView view, PlayerShipConfig shipConfig) : base(view){
			this.shipConfig = shipConfig;

			movementController = new PhysicMovementController(
				this,
				new PhysicMovementController.Config(
					shipConfig.MovementDumping,
					shipConfig.RotationDumping));

			var bulletFactory = ServiceLocator.Resolve<IBulletFactory>();

			gun1ShootController = new ShootController.ShootController(
				this,
				bulletFactory,
				() => new BulletFactoryArgs(Position, Forward, movementController.Speed, movementController.AngularSpeed),
				shipConfig.Gun1FireRate,
				false
			);

			var laserFactory = ServiceLocator.Resolve<ILaserFactory>();

			gun2ShootController = new ChargableShootController(
				this,
				laserFactory,
				() => new LaserFactoryArgs(Position, Forward, this),
				shipConfig.Gun2FireRate,
				true,
				shipConfig.Gun2MaxCharges,
				shipConfig.Gun2ChargeTime
			);
		}

		public void SetForwardSpeedThrottle(float value) =>
			movementController.SetForwardMovingForce(value * shipConfig.ForwardMovingMaxForce);

		public void SetAngularSpeedThrottle(float value) =>
			movementController.SetAngularSpeed(value * shipConfig.RotationSpeed);

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
	}
}