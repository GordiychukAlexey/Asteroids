using Core.GameWorld.MovementController;
using Core.GameWorld.WorldBoundsProvider;
using Core.Tools.InfinityWorld;
using UnityEngine;

namespace Core.GameWorld.Entities.Projectile.Bullet {
	public class BulletController : BaseProjectileController<IBulletView>, IBulletController {
		private readonly BulletConfig config;
		private readonly PhysicMovementController movementController;
		private readonly WorldObjectLifetimeController worldObjectLifetimeController;

		public BulletController(
			IBulletView view, 
			InfinityWorldSide worldSide,
			IWorldObjectController owner,
			BulletConfig config,
			IWorldBoundsProvider worldBoundsProvider,
			IInfinityWorld infinityWorld) 
			: base(view, owner,worldBoundsProvider,infinityWorld,worldSide){
			this.config = config;

			movementController = new PhysicMovementController(
				this,
				new PhysicMovementController.Config(0.0f, 0.0f));

			worldObjectLifetimeController = new WorldObjectLifetimeController(this, config.Lifetime);

			movementController.SetForwardSpeed(config.Speed);
		}
		
		protected override void TriggerEnterHandler(IWorldObjectController other){
			if (!config.IsImmortal){
				MarkToDispose();
			}
		}

		public void AddSpeed(Vector2 speed) => movementController.AddSpeed(speed);

		public void SetAngularSpeed(float speed) => movementController.SetAngularSpeed(speed);

		public override void Update(float dt){
			base.Update(dt);
			
			movementController.Update(dt);
			worldObjectLifetimeController.Update(dt);
		}

		public override void Dispose(){

			base.Dispose();
		}
	}
}