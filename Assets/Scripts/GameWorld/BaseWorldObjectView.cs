using System;
using Core.GameWorld;
using UnityEngine;

namespace GameWorld {
	public abstract class BaseWorldObjectView : MonoBehaviour, IWorldObjectView {
		public event Action<IWorldObjectView> OnTriggerEnterView;
		public event Action<IWorldObjectController> OnTriggerEnter;

		public Vector2 Position{
			get => transform.position;
			set => transform.position = value;
		}

		public Vector2 Forward{
			get => transform.up;
			set => transform.up = value;
		}

		public void SetParent(Transform parent) => transform.SetParent(parent);

		private void OnTriggerEnter2D(Collider2D other){
			IWorldObjectView otherView = other.attachedRigidbody.GetComponent<BaseWorldObjectView>();
//			Debug.Log($"{gameObject.name} {otherView.GetType().Name}");
			OnTriggerEnterView?.Invoke(otherView);
		}

		public void InvokeTriggerEnter(IWorldObjectController other){
//			Debug.Log($"{gameObject.name} {other.GetType().Name}");
			OnTriggerEnter?.Invoke(other);
		}

		public virtual void Dispose(){ }
	}
}