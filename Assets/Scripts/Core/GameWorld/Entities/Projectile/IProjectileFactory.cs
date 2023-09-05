namespace Core.GameWorld.Entities.Projectile {
//	public interface IProjectileFactory<TProjectileController, TProjectileFactoryArgs>
//		where TProjectileController : IProjectileController
////		where TProjectileView : IProjectileView
//		where TProjectileFactoryArgs : IProjectileFactoryArgs {
//		public TProjectileController Create(TProjectileFactoryArgs args);
//	}

	public interface IProjectileFactory {
		public IProjectileController Create(IProjectileFactoryArgs args);
	}
}