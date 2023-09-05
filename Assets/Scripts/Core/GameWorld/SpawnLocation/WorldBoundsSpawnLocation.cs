using Core.Tools.InfinityWorld;
using Core.Tools.ServiceLocator;
using UnityEngine;

namespace Core.GameWorld.SpawnLocation {
	public class WorldBoundsSpawnLocation : ISpawnLocation {
		private readonly Bounds spawnBounds;
		private readonly Vector2[] spawnBoundsVertexes;

		public WorldBoundsSpawnLocation(float sideExpand){
			Bounds worldBounds = ServiceLocator.Resolve<WorldBoundsProvider>().Bounds;

			spawnBounds = new Bounds(worldBounds.center, worldBounds.size + Vector3.one * 2 * sideExpand);

			spawnBoundsVertexes = new[]{
				(Vector2) spawnBounds.min,
				new Vector2(spawnBounds.min.x, spawnBounds.max.y),
				(Vector2) spawnBounds.max,
				new Vector2(spawnBounds.max.x, spawnBounds.min.y),
			};
		}

		public Vector2 GetNewSpawnPosition(){
			int side = Random.Range(0, 4);
			Vector2 firstSideVertex = spawnBoundsVertexes[side];
			Vector2 secondSideVertex = spawnBoundsVertexes[(side + 1) % 4];
			float sidePercent = Random.value;

			return Vector2.Lerp(firstSideVertex, secondSideVertex, sidePercent);
		}
	}
}