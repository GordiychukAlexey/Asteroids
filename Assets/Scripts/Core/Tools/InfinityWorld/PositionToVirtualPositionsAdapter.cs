//using System.Linq;
//using UnityEngine;
//
//namespace Core.Tools.InfinityWorld {
//	public class PositionToVirtualPositionsAdapter {
////		private readonly Bounds cameraBounds;
//		private readonly InfinityWorldPositionShiftProvider infinityWorldPositionShiftProvider;
//
//		public PositionToVirtualPositionsAdapter(){
////			cameraBounds = ServiceLocator.ServiceLocator.Resolve<WorldBoundsProvider>().Bounds;
//			infinityWorldPositionShiftProvider = ServiceLocator.ServiceLocator.Resolve<InfinityWorldPositionShiftProvider>();
//		}
//
//		public Vector2[] Adapt(Vector2 position) =>
//			InfinityWorld.VirtualWorldSides.Select(side => position + infinityWorldPositionShiftProvider.GetPositionShift(side)).ToArray();
////			new[]{
////				position + Vector2.left * cameraBounds.size.x,
////				position + Vector2.up * cameraBounds.size.y,
////				position + Vector2.right * cameraBounds.size.x,
////				position + Vector2.down * cameraBounds.size.y,
////			};
//	}
//}

