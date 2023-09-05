using Core.Tools.Pool;
using GameWorld.Entities.PlayerShip.Projectile.Bullet;
using UnityEngine;

namespace GameWorld.Entities.PlayerShip {
	public class PlayerBulletPool  : WorldObjectsPool<Bullet> {
		public PlayerBulletPool(
			Bullet prefab,
			Transform parentTransform,
			PoolType poolType,
			int maxPoolSize,
			bool collectionChecks) : base(
			prefab,
			parentTransform,
			poolType,
			maxPoolSize,
			collectionChecks){
		}
	}
}