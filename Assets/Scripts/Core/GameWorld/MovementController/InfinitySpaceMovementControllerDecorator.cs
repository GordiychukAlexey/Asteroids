//using Core.Tools;
//using Core.Tools.ServiceLocator;
//
//namespace Core.GameWorld.MovementController {
//	public class InfinitySpaceMovementControllerDecorator :IMovementController{
//		private readonly IMovementController movementController;
//		private readonly InfinitySpacePositionAdapter infinitySpacePositionAdapter;
//		public InfinitySpaceMovementControllerDecorator(IMovementController movementController){
//			this.movementController = movementController;
//			
//			infinitySpacePositionAdapter = ServiceLocator.Resolve<InfinitySpacePositionAdapter>();
//		}
//
//		public void Update(float dt){
//			movementController.Update(dt);
//			
//			movementController.Position = infinitySpacePositionAdapter.AdaptPosition(
//				movementController.Position);
//		}
//	}
//}

