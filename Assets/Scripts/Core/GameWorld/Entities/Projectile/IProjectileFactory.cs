namespace Core.GameWorld.Entities.Projectile {
	public interface IProjectileFactory {
		public IProjectileController Create(IProjectileFactoryArgs args);
	}
}