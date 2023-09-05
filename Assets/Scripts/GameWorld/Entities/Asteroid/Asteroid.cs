using Core.GameWorld.Entities.Asteroid;
using Core.Tools.Pool;
using UnityEngine.Pool;

namespace GameWorld.Entities.Asteroid {
	public class Asteroid : BaseWorldObjectView, IAsteroidView, IWorldObjectPoolable<Asteroid> {
		private IObjectPool<Asteroid> pool;

		public void OnSpawned(IObjectPool<Asteroid> pool){
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