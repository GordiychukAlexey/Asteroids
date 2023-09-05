using UnityEngine;

namespace Core.GameWorld {
	public interface ISpawnLocation {
		public Vector2 GetNewSpawnPosition();
	}
}