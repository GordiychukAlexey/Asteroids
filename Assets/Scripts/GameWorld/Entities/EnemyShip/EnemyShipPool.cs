using Core.Tools.Pool;
using UnityEngine;

namespace GameWorld.Entities.EnemyShip {
	public class EnemyShipPool : WorldObjectsPool<EnemyShip> {
		public EnemyShipPool(
			EnemyShip prefab,
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