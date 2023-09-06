namespace Core.GameWorld.Entities.EnemyShip {
	public interface IEnemyShipController : IDestroyableWorldObjectController {
		void SetTarget(IWorldObjectController target);
	}
}