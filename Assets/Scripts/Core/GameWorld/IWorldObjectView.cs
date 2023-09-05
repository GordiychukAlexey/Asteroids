using System;
using UnityEngine;

namespace Core.GameWorld {
	public interface IWorldObjectView : IDisposable {
		public event EventHandler<Collider2D> OnTriggerEnter;

		public Vector2 Position{ get; set; }

		public Vector2 Forward{ get; set; }
//		public void Destroy();
	}
}