using UnityEngine;
using UnityEngine.Pool;

namespace Core.Tools.Pool {
//	[RequireComponent(typeof(TComponent))]
	public abstract class BasePooledItem<TComponent> : MonoBehaviour //, IPoolable<TComponent>
		where TComponent : Component {
		public TComponent targetComponent{ get; private set; }
		private IObjectPool<TComponent> pool;

		void Start(){
			targetComponent = GetComponent<TComponent>();
		}

		public void Initialize(TComponent targetComponent, IObjectPool<TComponent> pool){
			this.targetComponent = targetComponent;
			this.pool = pool;
		}

		public void Release() => pool.Release(targetComponent);


//		public void OnSetPool(IObjectPool<TComponent> pool){
//			throw new System.NotImplementedException();
//		}
//
//		public void ReturnedToPool(){
//			throw new System.NotImplementedException();
//		}
//
//		public void OnTakeFromPool(){
//			throw new System.NotImplementedException();
//		}
//
//		public void OnReturnedToPool(){
//			throw new System.NotImplementedException();
//		}
	}
}