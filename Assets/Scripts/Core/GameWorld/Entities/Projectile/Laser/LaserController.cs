using Core.GameWorld.WorldBoundsProvider;
using Core.Tools.InfinityWorld;

namespace Core.GameWorld.Entities.Projectile.Laser {
	public class LaserController : BaseProjectileController<ILaserView>, ILaserController {
		private readonly LaserConfig config;
		private readonly WorldObjectLifetimeController worldObjectLifetimeController;
		private readonly IInfinityWorld infinityWorld;

		public LaserController(
			ILaserView view, 
			InfinityWorldSide worldSide, 
			LaserConfig config,
			IWorldObjectController owner,
			IWorldBoundsProvider worldBoundsProvider,
			IInfinityWorld infinityWorld)
			: base(view,owner, worldBoundsProvider, infinityWorld, worldSide){
			this.config = config;
			worldObjectLifetimeController = new WorldObjectLifetimeController(this, config.Lifetime);

			owner.OnDispose += OwnerDisposeHandler;

			this.infinityWorld = infinityWorld;
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