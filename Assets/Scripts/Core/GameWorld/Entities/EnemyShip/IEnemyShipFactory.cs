using Core.GameWorld.Entities.EnemyShip;
using Core.Tools.WorldObjectFactory;

namespace Core.GameWorld.Entities.Asteroid {
	public interface IEnemyShipFactory : IWorldObjectFactory<IEnemyShipController, IEnemyShipFactoryArgs> {
		public IEnemyShipController Create(IEnemyShipFactoryArgs args);
	}
}