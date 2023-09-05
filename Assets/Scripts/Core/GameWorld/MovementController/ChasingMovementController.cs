using Core.Tools;
using Core.Tools.InfinityWorld;
using Core.Tools.ServiceLocator;
using UnityEngine;

namespace Core.GameWorld.MovementController {
	public class ChasingMovementController : BaseMovementController<ChasingMovementController.Config> {
//		private readonly IWorldObjectController self;
		private IWorldObjectController target;

//		private readonly Config config;
//		private readonly PositionToVirtualPositionsAdapter positionToVirtualPositionsAdapter;
		private readonly InfinityWorld infinityWorld;

		public ChasingMovementController(IWorldObjectController self, Config config) : base(self, config){
//			positionToVirtualPositionsAdapter = ServiceLocator.Resolve<PositionToVirtualPositionsAdapter>();
			infinityWorld = ServiceLocator.Resolve<InfinityWorld>();
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


//				//todo remove linq
//				Vector2 closestPosition = positionToVirtualPositionsAdapter
//										 .Adapt(target.Position)
//										 .Concat(new Vector2[]{target.Position})
//										 .OrderBy(position => Vector2.SqrMagnitude(position - self.Position))
//										 .First();


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