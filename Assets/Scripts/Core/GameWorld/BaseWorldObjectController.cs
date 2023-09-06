using System;
using Core.Tools.InfinityWorld;
using UnityEngine;

namespace Core.GameWorld {
	public abstract class BaseWorldObjectController<TView> : IWorldObjectController
		where TView : class, IWorldObjectView {
		public event Action<IWorldObjectController> OnDispose;

		protected readonly TView view;

		private readonly InfinityWorldPortal infinityWorldPortal;

		protected BaseWorldObjectController(TView view, InfinityWorldSide worldSide = InfinityWorldSide.Center){
			this.view = view;
			WorldSide = worldSide;

			infinityWorldPortal = new InfinityWorldPortal(this);

			view.OnTriggerEnterView += TriggerEnterHandler;
			view.OnTriggerEnter += TriggerEnterHandler;
		}

		private void TriggerEnterHandler(IWorldObjectView other) => other.InvokeTriggerEnter(this);

		protected virtual void TriggerEnterHandler(IWorldObjectController other){ }

		public Vector2 Position{
			get => view.Position;
			set => view.Position = value;
		}

		public Vector2 Forward{
			get => view.Forward;
			set => view.Forward = value;
		}

		public InfinityWorldSide WorldSide{ get; }

		public bool IsVirtual => WorldSide != InfinityWorldSide.Center;

		public bool IsWasInWorldBounds => infinityWorldPortal.IsWasInWorldBounds;

		public virtual void Update(float dt){
			infinityWorldPortal.Update(dt);
		}

		public virtual void Dispose(){
			OnDispose?.Invoke(this);

			view.OnTriggerEnterView -= TriggerEnterHandler;
			view.OnTriggerEnter -= TriggerEnterHandler;

			view.Dispose();
		}
	}
}