using Core.Tools.WorldObjectFactory;

namespace Core.GameWorld.Entities.EnemyShip {
	public interface IEnemyShipFactory : IWorldObjectFactory<IEnemyShipController, IEnemyShipFactoryArgs> { }
}