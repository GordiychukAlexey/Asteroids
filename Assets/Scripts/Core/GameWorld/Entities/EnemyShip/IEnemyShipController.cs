namespace Core.GameWorld.Entities.EnemyShip {
	public interface IEnemyShipController : IWorldObjectController {
		void SetTarget(IWorldObjectController target);
	}
}