using Core.GameWorld.Entities.Bullet.PlayerBullet;
using Core.Tools.Pool;
using UnityEngine.Pool;

namespace GameWorld.Entities.PlayerShip.Projectile.Bullet {
	public class Bullet : BaseWorldObjectView, IPlayerBulletView , IWorldObjectPoolable<Bullet> {
		private IObjectPool<Bullet> pool;

		public void OnSpawned(IObjectPool<Bullet> pool)
		{
			this.pool = pool;
		}

		public void OnDespawned()
		{
			pool = null;
		}
		
		public override void Dispose()
		{
			base.Dispose();
			
			pool.Release(this);
		}
	}
}