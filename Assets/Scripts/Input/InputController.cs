using System;
using Core.Tools.ServiceLocator;
using UnityEngine;
using PlayerInput = Core.Input.PlayerInput;

namespace Input {
	public class InputController : IDisposable {
		private readonly InputActions inputActions;
		private readonly PlayerInput playerInput;

		public InputController(){
			inputActions = ServiceLocator.Resolve<InputActions>();
			inputActions.Enable();

			playerInput = ServiceLocator.Resolve<PlayerInput>();
		}

		public void Update(){
			Vector2 playerMoveInputValue = inputActions.Player.Move.ReadValue<Vector2>();

			playerInput.MoveForward = Mathf.Clamp01(playerMoveInputValue.y);
			playerInput.RotateAround = Mathf.Clamp(playerMoveInputValue.x, -1.0f, 1.0f);

			playerInput.Shoot1 = inputActions.Player.Shoot1.IsPressed();
			playerInput.Shoot2 = inputActions.Player.Shoot2.IsPressed();
		}

		public void Dispose(){
			inputActions.Disable();
			inputActions.Dispose();
		}
	}
}