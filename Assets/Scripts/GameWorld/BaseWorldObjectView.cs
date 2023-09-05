using System;
using Core.GameWorld;
using UnityEngine;

namespace GameWorld {
	public abstract class BaseWorldObjectView : MonoBehaviour, IWorldObjectView {
		public event EventHandler<Collider2D> OnTriggerEnter;

		public Vector2 Position{
			get => transform.position;
			set => transform.position = value;
		}

		public Vector2 Forward{
			get => transform.up;
			set => transform.up = value;
		}

		public void SetParent(Transform parent) => transform.SetParent(parent);

		private void OnTriggerEnter2D(Collider2D other) => OnTriggerEnter?.Invoke(this, other);

		public virtual void Dispose(){ }
	}
}