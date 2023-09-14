using System;
using Core.GameWorld.MovementController;
using Core.GameWorld.WorldBoundsProvider;
using Core.Tools.InfinityWorld;

namespace Core.GameWorld.Entities.EnemyShip {
	public class EnemyShipController : BaseWorldObjectController<IEnemyShipView>, IEnemyShipController {
		public event Action<IDestroyableWorldObjectController> OnDestroy;
		
		private readonly EnemyShipConfig shipConfig;

		private readonly ChasingMovementController movementController;

		public EnemyShipController(
			IEnemyShipView view, 
			EnemyShipConfig shipConfig,
			IWorldBoundsProvider worldBoundsProvider,
			IInfinityWorld infinityWorld) : base(view, worldBoundsProvider, infinityWorld){
			this.shipConfig = shipConfig;

			movementController = new ChasingMovementController(
				this,
				new ChasingMovementController.Config(
					shipConfig.Speed,
					shipConfig.MaxRotateSpeed),
				infinityWorld);
		}
		
		protected override void TriggerEnterHandler(IWorldObjectController other){
			OnDestroy?.Invoke(this);
			
			MarkToDispose();
		}

		public void SetTarget(IWorldObjectController target) => movementController.SetTarget(target);

		public override void Update(float dt){
			base.Update(dt);
			
			movementController.Update(dt);
		}
	}
}