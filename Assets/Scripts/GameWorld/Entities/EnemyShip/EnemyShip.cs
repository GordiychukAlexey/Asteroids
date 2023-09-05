using System;
using Core.GameWorld.Entities.EnemyShip;
using Core.Tools.Pool;
using UnityEngine.Pool;

namespace GameWorld.Entities.EnemyShip {
	public class EnemyShip : BaseWorldObjectView, IEnemyShipView , IWorldObjectPoolable<EnemyShip> {
		private IObjectPool<EnemyShip> pool;
		
//		public void OnSetPool(IObjectPool<EnemyShip> pool){
//			this.pool = pool;
//		}

//		public void ReturnToPool(){
//			pool.Release(this);
//		}
//
//		public void OnTakeFromPool(){
//		}
//
//		public void OnReturnedToPool(){
//		}

		public void OnSpawned(IObjectPool<EnemyShip> pool)
		{
//			transform.position = Vector3.zero;
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