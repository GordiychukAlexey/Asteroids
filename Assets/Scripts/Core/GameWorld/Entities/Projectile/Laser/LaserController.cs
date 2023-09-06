using Core.Tools.InfinityWorld;

namespace Core.GameWorld.Entities.Projectile.Laser {
	public class LaserController : BaseProjectileController<ILaserView>, ILaserController {
		private readonly LaserConfig config;
		private readonly WorldObjectLifetimeController worldObjectLifetimeController;
		private readonly InfinityWorld infinityWorld;

		public LaserController(ILaserView view, InfinityWorldSide worldSide, LaserConfig config, IWorldObjectController owner)
			: base(view,owner, worldSide){
			this.config = config;
			worldObjectLifetimeController = new WorldObjectLifetimeController(this, config.Lifetime);
			infinityWorld = Tools.ServiceLocator.ServiceLocator.Resolve<InfinityWorld>();

			owner.OnDispose += OwnerDisposeHandler;
		}

		public override void Update(float dt){
			base.Update(dt);
			worldObjectLifetimeController.Update(dt);

			view.Position = infinityWorld.ToSidePosition(Owner.Position, WorldSide);
			view.Forward = Owner.Forward;
		}

		private void OwnerDisposeHandler(IWorldObjectController obj) => Dispose();

		public override void Dispose(){
			Owner.OnDispose -= OwnerDisposeHandler;

			base.Dispose();
		}
	}
}