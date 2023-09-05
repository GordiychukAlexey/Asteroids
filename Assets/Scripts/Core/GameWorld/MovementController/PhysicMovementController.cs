using UnityEngine;

namespace Core.GameWorld.MovementController {
	public class PhysicMovementController : BaseMovementController<PhysicMovementController.Config> {
		public Vector2 Speed{ get; private set; } = Vector2.zero;
		public float AngularSpeed{ get; private set; } = 0.0f;
		private float forwardMovingForce = 0.0f;

		public PhysicMovementController(IWorldObjectController self, Config config) : base(self, config){ }

		public void SetForwardSpeed(float value) => Speed = self.Forward * value;

		public void SetSpeed(Vector2 value) => Speed = value;

		public void SetAngularSpeed(float value) => AngularSpeed = value;

		public void AddForwardSpeed(float value) => AddSpeed(self.Forward * value);

		public void SetForwardMovingForce(float value) => forwardMovingForce = value;

		public void AddSpeed(Vector2 value) => Speed += value;

		public void AddAngularSpeed(float value) => AngularSpeed += value;

		public override void Update(float dt){
			self.Forward = Quaternion.AngleAxis(AngularSpeed * dt, Vector3.back) * self.Forward;

			Speed += self.Forward * (forwardMovingForce * dt);
			self.Position += Speed * dt;

			AngularSpeed *= 1.0f - config.movementDumping * dt;
			Speed *= 1.0f - config.movementDumping * dt;
		}

		public class Config {
			public readonly float movementDumping;
			public readonly float rotationDumping;

			public Config(float movementDumping, float rotationDumping){
				this.movementDumping = movementDumping;
				this.rotationDumping = rotationDumping;
			}
		}
	}
}