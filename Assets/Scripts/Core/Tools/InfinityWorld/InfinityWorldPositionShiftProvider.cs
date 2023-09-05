//using System.Collections.Generic;
//using UnityEngine;
//
//namespace Core.Tools.InfinityWorld {
//	public class InfinityWorldPositionShiftProvider {
//		private readonly Dictionary<InfinityWorldSide, Vector2> positionShift;
//
//		public InfinityWorldPositionShiftProvider(){
//			var worldBounds = ServiceLocator.ServiceLocator.Resolve<WorldBoundsProvider>().Bounds;
//
//			positionShift = new Dictionary<InfinityWorldSide, Vector2>(){
//				{InfinityWorldSide.Center, Vector2.zero},
//				{InfinityWorldSide.Left, Vector2.left * worldBounds.size.x},
//				{InfinityWorldSide.Top, Vector2.up * worldBounds.size.y},
//				{InfinityWorldSide.Right, Vector2.right * worldBounds.size.x},
//				{InfinityWorldSide.Down, Vector2.down * worldBounds.size.y},
//			};
//		}
//
//		public Vector2 GetPositionShift(InfinityWorldSide infinityWorldSide) => positionShift[infinityWorldSide];
//	}
//}

