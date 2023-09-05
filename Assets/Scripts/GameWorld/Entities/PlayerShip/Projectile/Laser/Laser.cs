using Core.GameWorld.Entities.Projectile.Laser;
using Core.Tools.Pool;
using UnityEngine.Pool;

namespace GameWorld.Entities.PlayerShip.Projectile.Laser {
	public class Laser  : BaseWorldObjectView, ILaserView , IWorldObjectPoolable<Laser> {
		private IObjectPool<Laser> pool;

		public void OnSpawned(IObjectPool<Laser> pool)
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