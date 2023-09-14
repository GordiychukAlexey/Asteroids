using UnityEngine;

namespace Core.GameWorld.WorldBoundsProvider {
	public interface IWorldBoundsProvider {
		public Bounds Bounds{ get; }
	}
}