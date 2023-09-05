using Core.GameWorld.Entities.PlayerShip;
using Core.Tools;
using Core.Tools.ServiceLocator;

namespace Core.Input {
	public class PlayerShipInputController {
		private IPlayerShipController playerShipController;

		private readonly PlayerInput playerInput;

		public PlayerShipInputController(IPlayerShipController playerShipController){
			this.playerShipController = playerShipController;

			playerInput = ServiceLocator.Resolve<PlayerInput>();
		}

		public void Update(float dt){
			playerShipController.SetForwardSpeedThrottle(playerInput.MoveForward);
			playerShipController.SetAngularSpeedThrottle(playerInput.RotateAround);
			playerShipController.Gun1IsSpawnActive = playerInput.Shoot1;
			playerShipController.Gun2IsSpawnActive = playerInput.Shoot2;
		}
	}
}