using Core.GameWorld.Entities.EnemyShip;
using Core.Tools.Pool;
using UnityEngine.Pool;

namespace GameWorld.Entities.EnemyShip {
	public class EnemyShip : BaseWorldObjectView, IEnemyShipView, IWorldObjectPoolable<EnemyShip> {
		private IObjectPool<EnemyShip> pool;

		public void OnSpawned(IObjectPool<EnemyShip> pool){
			this.pool = pool;
		}

		public void OnDespawned(){
			pool = null;
		}

		public override void Dispose(){
			base.Dispose();

			pool.Release(this);
		}
	}
}