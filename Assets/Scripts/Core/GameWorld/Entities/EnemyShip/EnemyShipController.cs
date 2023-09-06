using System;
using Core.GameWorld.MovementController;

namespace Core.GameWorld.Entities.EnemyShip {
	public class EnemyShipController : BaseWorldObjectController<IEnemyShipView>, IEnemyShipController {
		public event Action<IDestroyableWorldObjectController> OnDestroy;
		
		private readonly EnemyShipConfig shipConfig;

		private readonly ChasingMovementController movementController;

		public EnemyShipController(IEnemyShipView view, EnemyShipConfig shipConfig) : base(view){
			this.shipConfig = shipConfig;

			movementController = new ChasingMovementController(
				this,
				new ChasingMovementController.Config(
					shipConfig.Speed,
					shipConfig.MaxRotateSpeed));
		}
		
		protected override void TriggerEnterHandler(IWorldObjectController other){
			OnDestroy?.Invoke(this);
			
			MarkToDispose();
		}

		public void SetTarget(IWorldObjectController target) => movementController.SetTarget(target);

		public override void Update(float dt){
			movementController.Update(dt);

			base.Update(dt);
		}
	}
}