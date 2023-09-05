using Core.Tools.InfinityWorld;

namespace Core.GameWorld.Entities.Projectile.Laser {
	public class LaserController : BaseWorldObjectController<ILaserView>, ILaserController {
		private readonly LaserConfig config;
		private readonly IWorldObjectController ownerWorldObjectController;
		private readonly WorldObjectLifetimeController worldObjectLifetimeController;
		private readonly InfinityWorld infinityWorld;

		public LaserController(ILaserView view, InfinityWorldSide worldSide, LaserConfig config, IWorldObjectController ownerWorldObjectController)
			: base(view, worldSide){
			this.config = config;
			this.ownerWorldObjectController = ownerWorldObjectController;
			worldObjectLifetimeController = new WorldObjectLifetimeController(this, config.Lifetime);
			infinityWorld = Tools.ServiceLocator.ServiceLocator.Resolve<InfinityWorld>();

			ownerWorldObjectController.OnDispose += OwnerDisposeHandler;
		}

		public override void Update(float dt){
			base.Update(dt);
			worldObjectLifetimeController.Update(dt);

			view.Position = infinityWorld.ToSidePosition(ownerWorldObjectController.Position, WorldSide);
			view.Forward = ownerWorldObjectController.Forward;
		}

		private void OwnerDisposeHandler(IWorldObjectController obj) => Dispose();

		public override void Dispose(){
			ownerWorldObjectController.OnDispose -= OwnerDisposeHandler;

			base.Dispose();
		}
	}
}