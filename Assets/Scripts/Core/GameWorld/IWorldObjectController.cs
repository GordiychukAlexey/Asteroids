using System;
using Core.Tools.InfinityWorld;
using UnityEngine;

namespace Core.GameWorld {
	public interface IWorldObjectController : IUpdatable, IDisposable {
		public event Action<IWorldObjectController> OnDispose;

		public Vector2 Position{ get; set; }
		public Vector2 Forward{ get; set; }

		public InfinityWorldSide WorldSide{ get; }
		public bool IsVirtual{ get; }

		public bool IsWasInWorldBounds{ get; }

		public void MarkToDispose();
	}
}