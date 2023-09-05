using System.Collections.Generic;
using System.Linq;
using Core.GameWorld;
using UnityEngine;

namespace Core.Tools.InfinityWorld {
	public class InfinityWorld {
		private readonly Dictionary<InfinityWorldSide, Vector2> positionShift;
		private readonly Dictionary<InfinityWorldSide, Bounds> bounds;

		public InfinityWorld(Bounds worldBounds){
			positionShift = new Dictionary<InfinityWorldSide, Vector2>(){
				{InfinityWorldSide.Center, Vector2.zero},
				{InfinityWorldSide.Left, Vector2.left * worldBounds.size.x},
				{InfinityWorldSide.Top, Vector2.up * worldBounds.size.y},
				{InfinityWorldSide.Right, Vector2.right * worldBounds.size.x},
				{InfinityWorldSide.Down, Vector2.down * worldBounds.size.y},
			};

			bounds = positionShift.ToDictionary(
				pair => pair.Key,
				pair => new Bounds(
					(Vector2) worldBounds.center + positionShift[pair.Key],
					worldBounds.size));
		}

		public void AdaptPosition(IWorldObjectController worldObjectController){
			var boundsTmp = bounds[worldObjectController.WorldSide];
			var x = new Vector2(
				boundsTmp.min.x +
				((worldObjectController.Position.x - boundsTmp.min.x) % boundsTmp.size.x
			   + boundsTmp.size.x) % boundsTmp.size.x,
				boundsTmp.min.y +
				((worldObjectController.Position.y - boundsTmp.min.y) % boundsTmp.size.y
			   + boundsTmp.size.y) % boundsTmp.size.y);

			worldObjectController.Position = x;
		}

		public Vector2 ToSidePosition(Vector2 position, InfinityWorldSide side) => position + GetPositionShift(side);

		public Vector2[] ToVirtualPositions(Vector2 position) => VirtualWorldSides.Select(side => ToSidePosition(position, side)).ToArray();

		public Vector2 GetPositionShift(InfinityWorldSide side) => positionShift[side];

		public static readonly InfinityWorldSide[] WorldSides = {
			InfinityWorldSide.Center,
			InfinityWorldSide.Left,
			InfinityWorldSide.Top,
			InfinityWorldSide.Right,
			InfinityWorldSide.Down,
		};

		public static readonly InfinityWorldSide[] VirtualWorldSides = {
			InfinityWorldSide.Left,
			InfinityWorldSide.Top,
			InfinityWorldSide.Right,
			InfinityWorldSide.Down,
		};
	}
}