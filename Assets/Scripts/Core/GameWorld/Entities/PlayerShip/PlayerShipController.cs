using Core.GameWorld.Entities.Projectile.Bullet;
using Core.GameWorld.Entities.Projectile.Laser;
using Core.GameWorld.MovementController;
using Core.GameWorld.ShootController;
using Core.Tools.ServiceLocator;
using UnityEngine;

namespace Core.GameWorld.Entities.PlayerShip {
	public class PlayerShipController : BaseWorldObjectController<IPlayerShipView>, IPlayerShipController {
		private readonly PlayerShipConfig shipConfig;

		private readonly PhysicMovementController movementController;

		private readonly ShootController.ShootController gun1ShootController;
		private readonly ChargableShootController gun2ShootController;

		private readonly PlayerGunConfig gun1Config;
		private readonly PlayerGunConfig gun2Config;

		public Vector2 Speed => movementController.Speed;
		public float AngularSpeed => movementController.AngularSpeed;
		public int LaserCharges => gun2ShootController.CurrentCharges;
		public float LaserChargeTimeLeft => gun2ShootController.ChargeTimeLeft;

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
				() => new BulletFactoryArgs(Position, Forward, this, movementController.Speed, movementController.AngularSpeed),
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
		
		protected override void TriggerEnterHandler(IWorldObjectController other){
			MarkToDispose();
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
			base.Update(dt);
			
			movementController.Update(dt);

			gun1ShootController.Update(dt);
			gun2ShootController.Update(dt);
		}

		public override void Dispose(){
			gun1ShootController.Dispose();
			gun2ShootController.Dispose();
				
			base.Dispose();
		}
	}
}