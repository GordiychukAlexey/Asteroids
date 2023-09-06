using Core.Tools.Pool;
using UnityEngine;

namespace GameWorld.Entities.PlayerShip.Projectile.Laser {
	public class LaserPool : WorldObjectsPool<Laser> {
		public LaserPool(
			Laser prefab,
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