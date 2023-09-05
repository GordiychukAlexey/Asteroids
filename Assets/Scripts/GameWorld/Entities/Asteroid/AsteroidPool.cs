using Core.Tools.Pool;
using UnityEngine;

namespace GameWorld.Entities.Asteroid {
	public class AsteroidPool : WorldObjectsPool<Asteroid> {
		public AsteroidPool(
			Asteroid prefab,
			Transform parentTransform,
			PoolType poolType,
			int maxPoolSize,
			bool collectionChecks) : base(
			prefab,
			parentTransform,
			poolType,
			maxPoolSize,
			collectionChecks){ }
	}
}