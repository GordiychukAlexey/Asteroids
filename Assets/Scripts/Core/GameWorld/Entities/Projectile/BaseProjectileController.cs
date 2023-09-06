using Core.Tools.InfinityWorld;

namespace Core.GameWorld.Entities.Projectile {
	public abstract class BaseProjectileController<TView> : BaseWorldObjectController<TView>, IProjectileController
		where TView: class, IProjectileView{
		public IWorldObjectController Owner{ get; }
		
		public BaseProjectileController(TView view, IWorldObjectController owner, InfinityWorldSide worldSide = InfinityWorldSide.Center) :
			base(view, worldSide){
			Owner = owner;
		}
	}
}