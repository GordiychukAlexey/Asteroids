using UnityEngine;
using UnityEngine.Pool;

namespace Core.Tools.Pool {
	public interface IWorldObjectPoolable<T> where T : class {
//		public void OnSetPool(IObjectPool<T> pool);
//		public void ReturnedToPool();
//		public void OnTakeFromPool();
//		public void OnReturnedToPool();

		public void OnSpawned(IObjectPool<T> pool);
		public void OnDespawned();

		public void SetParent(Transform transform);
	}
}