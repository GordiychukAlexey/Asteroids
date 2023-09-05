using System;
using Core.Tools;
using Core.Tools.ServiceLocator;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Core.Input.PlayerInput;

namespace Input {
	public class InputController : IDisposable {
		private readonly InputActions inputActions;
		private readonly PlayerInput playerInput;

		public InputController(){
			inputActions = ServiceLocator.Resolve<InputActions>();
			inputActions.Enable();

//			inputActions.Player.Shoot1.started += Shoot1OnStarted;
//			inputActions.Player.Shoot1.canceled += Shoot1OnCanceled;
//			
//			inputActions.Player.Shoot2.started += Shoot2OnStarted;
//			inputActions.Player.Shoot2.canceled += Shoot2OnCanceled;

			playerInput = ServiceLocator.Resolve<PlayerInput>();
		}

		public void Update(){
			Vector2 playerMoveInputValue = inputActions.Player.Move.ReadValue<Vector2>();

			playerInput.MoveForward = Mathf.Clamp01(playerMoveInputValue.y);
			playerInput.RotateAround = Mathf.Clamp(playerMoveInputValue.x, -1.0f, 1.0f);

			playerInput.Shoot1 = inputActions.Player.Shoot1.IsPressed();
			playerInput.Shoot2 = inputActions.Player.Shoot2.IsPressed();

//			Debug.Log(playerInput.Shoot1);
		}

//		private void Shoot1OnStarted(InputAction.CallbackContext obj){
//			playerInput.Shoot1 = true;
//		}
//
//		private void Shoot1OnCanceled(InputAction.CallbackContext obj){
//			playerInput.Shoot1 = false;
//		}
//
//		private void Shoot2OnStarted(InputAction.CallbackContext obj){
//			playerInput.Shoot2 = true;
//		}
//
//		private void Shoot2OnCanceled(InputAction.CallbackContext obj){
//			playerInput.Shoot2 = false;
//		}

		public void Dispose(){
//			inputActions.Player.Shoot1.started -= Shoot1OnStarted;
//			inputActions.Player.Shoot1.canceled -= Shoot1OnCanceled;

//			inputActions.Player.Shoot2.started = Shoot2OnStarted;
//			inputActions.Player.Shoot2.canceled = Shoot2OnCanceled;

			inputActions.Disable();
			inputActions.Dispose();
		}
	}
}