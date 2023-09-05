using Core.GameWorld.Entities.Projectile;

namespace Core.GameWorld.Entities.Bullet {
	public interface ILaserFactoryArgs : IProjectileFactoryArgs {
		public IWorldObjectController Owner{ get; }
	}
}