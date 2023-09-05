using UnityEngine;

namespace Core.GameWorld.SpawnLocation {
	public interface ISpawnLocation {
		public Vector2 GetNewSpawnPosition();
	}
}