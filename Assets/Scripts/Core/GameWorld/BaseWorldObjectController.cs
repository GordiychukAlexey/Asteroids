using System;
using Core.Tools.InfinityWorld;
using UnityEngine;

namespace Core.GameWorld {
	public abstract class BaseWorldObjectController<TView> : IWorldObjectController
		where TView : class, IWorldObjectView {
		public event Action<IWorldObjectController> OnDispose;

		protected readonly TView view;

		private readonly InfinityWorldPortal infinityWorldPortal;

		public bool IsWasInWorldBounds => infinityWorldPortal.IsWasInWorldBounds;

		protected BaseWorldObjectController(TView view, InfinityWorldSide worldSide = InfinityWorldSide.Center){
			this.view = view;
			WorldSide = worldSide;

			infinityWorldPortal = new InfinityWorldPortal(this);
		}

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

		//		public Vector2[] GetVirtualPositions(){
//			throw new NotImplementedException();
//		}

		public virtual void Update(float dt){
			infinityWorldPortal.Update(dt);
		}

		public virtual void Dispose(){
			OnDispose?.Invoke(this);

			view.Dispose();
		}
	}
}