using UnityEngine;
using UnityEngine.Pool;

namespace Core.Tools.Pool {
	public abstract class BasePooledItem<TComponent> : MonoBehaviour
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
	}
}