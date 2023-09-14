using Core.Tools.InfinityWorld;
using UnityEngine;

namespace Core.GameWorld.MovementController {
	public class ChasingMovementController : BaseMovementController<ChasingMovementController.Config> {
		private IWorldObjectController target;

		private readonly IInfinityWorld infinityWorld;

		public ChasingMovementController(IWorldObjectController self, Config config,
			IInfinityWorld infinityWorld) : base(self, config){
			this.infinityWorld = infinityWorld;
		}

		public void SetTarget(IWorldObjectController target){
			this.target = target;
		}

		public override void Update(float dt){
			if (target != null){
				Vector2 closestPosition = target.Position;
				float closestPositionSqrMagnitude = (closestPosition - self.Position).sqrMagnitude;

				if (self.IsWasInWorldBounds){
					foreach (var virtualPosition in infinityWorld.ToVirtualPositions(target.Position)){
						float virtualPositionSqrMagnitude = (virtualPosition - self.Position).sqrMagnitude;
						if (virtualPositionSqrMagnitude < closestPositionSqrMagnitude){
							closestPosition = virtualPosition;
							closestPositionSqrMagnitude = virtualPositionSqrMagnitude;
						}
					}
				}

				self.Forward = Vector3.RotateTowards(self.Forward, closestPosition - self.Position,
													 Mathf.Deg2Rad * config.maxRotateSpeed * dt,
													 0.0f);

				self.Position += self.Forward * (config.speed * dt);
			}
		}

		public class Config {
			public readonly float speed;
			public readonly float maxRotateSpeed;

			public Config(float speed, float maxRotateSpeed){
				this.speed = speed;
				this.maxRotateSpeed = maxRotateSpeed;
			}
		}
	}
}