using System;
using UnityEngine;

namespace Core.GameWorld.Entities.PlayerShip {
	[Serializable]
	public class PlayerShipConfig {
		[SerializeField] private float forwardMovingMaxForce = 20.0f;
		[SerializeField] private float rotationSpeed = 270.0f;
		[SerializeField] private float rotationMaxForce = 400.0f;
		[SerializeField] private float movementDumping = 0.1f;
		[SerializeField] private float rotationDumping = 0.6f;
		[SerializeField] private float gun1FireRate = 4.0f;
		[SerializeField] private float gun2FireRate = 1.0f;
		[SerializeField] private int gun2MaxCharges = 2;
		[SerializeField] private float gun2ChargeTime = 2.0f;

		public float ForwardMovingMaxForce => forwardMovingMaxForce;
		public float RotationSpeed => rotationSpeed;
		public float RotationMaxForce => rotationMaxForce;
		public float MovementDumping => movementDumping;
		public float RotationDumping => rotationDumping;
		public float Gun1FireRate => gun1FireRate;
		public float Gun2FireRate => gun2FireRate;
		public int Gun2MaxCharges => gun2MaxCharges;
		public float Gun2ChargeTime => gun2ChargeTime;
	}
}