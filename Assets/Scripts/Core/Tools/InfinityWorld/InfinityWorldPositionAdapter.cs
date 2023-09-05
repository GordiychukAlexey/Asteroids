//using System;
//using UnityEngine;
//
//namespace Core.Tools.InfinityWorld {
//	public class InfinityWorldPositionAdapter {
//		private readonly Bounds bounds;
//
//		public InfinityWorldPositionAdapter(){
//			bounds = ServiceLocator.ServiceLocator.Resolve<WorldBoundsProvider>().Bounds;
//		}
//
//		public Vector2 AdaptPosition(Vector2 position, InfinityWorldSide infinityWorldSide = InfinityWorldSide.Center){
//			var x = new Vector2(
//				bounds.min.x +
//				((position.x - bounds.min.x) % bounds.size.x
//			   + bounds.size.x) % bounds.size.x,
//				bounds.min.y +
//				((position.y - bounds.min.y) % bounds.size.y
//			   + bounds.size.y) % bounds.size.y);
//
//			switch (infinityWorldSide){
//				case InfinityWorldSide.Center:
//					return x;
//				case InfinityWorldSide.Left:
//					return x + Vector2.left * bounds.size.x;
//				case InfinityWorldSide.Top:
//					return x + Vector2.up * bounds.size.y;
//				case InfinityWorldSide.Right:
//					return x + Vector2.right * bounds.size.x;
//				case InfinityWorldSide.Down:
//					return x + Vector2.down * bounds.size.y;
//				default:
//					throw new ArgumentOutOfRangeException(nameof(infinityWorldSide), infinityWorldSide, null);
//			}
//		}
//	}
//}

