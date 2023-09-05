using Core.GameWorld.Entities.PlayerShip;

namespace Core.GameWorld {
	public class PlayerShipProvider {
		private IPlayerShipController playerShipController;

		public PlayerShipProvider(IPlayerShipController playerShipController){
			this.playerShipController = playerShipController;
		}

		public IPlayerShipController Provide() => playerShipController;
	}
}