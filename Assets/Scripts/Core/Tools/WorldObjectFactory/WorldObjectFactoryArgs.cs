using Core.Tools.InfinityWorld;
using UnityEngine;

namespace Core.Tools.WorldObjectFactory {
	public class WorldObjectFactoryArgs : IWorldObjectFactoryArgs {
		public Vector2 Position{ get; set; }
		public Vector2 Forward{ get; }
		public InfinityWorldSide WorldSide{ get; set; }

		public WorldObjectFactoryArgs(Vector2 position, Vector2 forward, InfinityWorldSide worldSide = InfinityWorldSide.Center){
			Position = position;
			Forward = forward;
			WorldSide = worldSide;
		}
	}
}