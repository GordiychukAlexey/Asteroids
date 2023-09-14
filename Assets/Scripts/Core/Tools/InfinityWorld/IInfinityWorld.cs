using Core.GameWorld;
using UnityEngine;

namespace Core.Tools.InfinityWorld
{
	public interface IInfinityWorld {
		public void AdaptPosition(IWorldObjectController worldObjectController);

		public Vector2 ToSidePosition(Vector2 position, InfinityWorldSide side);

		public Vector2[] ToVirtualPositions(Vector2 position);

		public Vector2 GetPositionShift(InfinityWorldSide side);
	}
}