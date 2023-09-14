using Core.GameWorld.Entities.PlayerShip;

namespace Core.Input {
	public class PlayerShipInputController {
		private IPlayerShipController playerShipController;

		private readonly PlayerInput playerInput;

		public PlayerShipInputController(
			IPlayerShipController playerShipController,
			PlayerInput playerInput){
			this.playerShipController = playerShipController;

			this.playerInput = playerInput;
		}

		public void Update(float dt){
			playerShipController.SetForwardSpeedThrottle(playerInput.MoveForward);
			playerShipController.SetAngularSpeedThrottle(playerInput.RotateAround);
			playerShipController.Gun1IsSpawnActive = playerInput.Shoot1;
			playerShipController.Gun2IsSpawnActive = playerInput.Shoot2;
		}
	}
}