using System;
using Core.GameWorld.MovementController;
using UnityEngine;

namespace Core.GameWorld.Entities.EnemyShip {
	public class EnemyShipController : BaseWorldObjectController<IEnemyShipView>, IEnemyShipController {
		public event Action<EnemyShipController> OnDispose;

		private readonly EnemyShipConfig shipConfig;

		private readonly ChasingMovementController movementController;

		public EnemyShipController(IEnemyShipView view, EnemyShipConfig shipConfig) : base(view){
			this.shipConfig = shipConfig;

			movementController = new ChasingMovementController(
				this,
				new ChasingMovementController.Config(
					shipConfig.Speed,
					shipConfig.MaxRotateSpeed));

			view.OnTriggerEnter += TriggerEnterHandler;
		}

		private void TriggerEnterHandler(object sender, Collider2D e){
			Dispose();
		}

		public void SetTarget(IWorldObjectController target) => movementController.SetTarget(target);

		public override void Update(float dt){
			movementController.Update(dt);

			base.Update(dt);
		}

		public override void Dispose(){
			OnDispose?.Invoke(this);

			view.OnTriggerEnter -= TriggerEnterHandler;

			base.Dispose();
		}
	}
}