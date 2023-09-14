using Core.GameWorld.WorldBoundsProvider;
using Core.Tools.InfinityWorld;

namespace Core.GameWorld.Entities.Projectile {
	public abstract class BaseProjectileController<TView> : BaseWorldObjectController<TView>, IProjectileController
		where TView: class, IProjectileView{
		public IWorldObjectController Owner{ get; }
		
		public BaseProjectileController(
			TView view, 
			IWorldObjectController owner, 
			IWorldBoundsProvider worldBoundsProvider,
			IInfinityWorld infinityWorld,
			InfinityWorldSide worldSide = InfinityWorldSide.Center) :
			base(view, worldBoundsProvider, infinityWorld, worldSide){
			Owner = owner;
		}
	}
}