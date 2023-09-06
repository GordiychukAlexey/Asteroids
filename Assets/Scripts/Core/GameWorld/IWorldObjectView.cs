using System;
using UnityEngine;

namespace Core.GameWorld {
	public interface IWorldObjectView : IDisposable {
		public event Action<IWorldObjectView> OnTriggerEnterView;
		public event Action<IWorldObjectController> OnTriggerEnter;
		public void InvokeTriggerEnter(IWorldObjectController other);

		public Vector2 Position{ get; set; }

		public Vector2 Forward{ get; set; }
	}
}