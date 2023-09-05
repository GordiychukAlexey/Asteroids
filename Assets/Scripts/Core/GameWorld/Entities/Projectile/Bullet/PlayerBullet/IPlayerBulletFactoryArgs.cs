using Core.Tools.WorldObjectFactory;

namespace Core.GameWorld.Entities.PlayerShip {
	public interface IPlayerBulletFactoryArgs : IWorldObjectFactoryArgs {
		public float MovingSpeed{ get; }
	}
}