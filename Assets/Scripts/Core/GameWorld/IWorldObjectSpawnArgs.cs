using Core.Tools.InfinityWorld;
using UnityEngine;

namespace Core.GameWorld {
	public interface IWorldObjectSpawnArgs {
		public Vector2 Position{ get; set; }
		public Vector2 Forward{ get; }
		public InfinityWorldSide WorldSide{ get; set; }
	}
}