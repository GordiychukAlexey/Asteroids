using System;
using Core.GameWorld;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Core.Tools.WorldObjectFactory {
	public abstract class WorldObjectFactory<TController, TView, TFactoryArgs> : IWorldObjectFactory<TController, TFactoryArgs>
		where TController : class, IWorldObjectController
		where TView : Object, IWorldObjectView
		where TFactoryArgs : IWorldObjectFactoryArgs {
		public event Action<TController> OnCreateEvent;

		private readonly Func<TView> getViewInstance;

		protected WorldObjectFactory(TView view) : this(() => Object.Instantiate(view)){ }

		protected WorldObjectFactory(IObjectPool<TView> viewPool) : this(viewPool.Get){ }

		private WorldObjectFactory(Func<TView> getViewInstance){
			this.getViewInstance = getViewInstance;
		}

		public TController Create(TFactoryArgs args){
			TView instance = CreateViewInstance(args);

			TController controller = CreateController(instance, args);

			OnCreateEvent?.Invoke(controller);

			return controller;
		}

		private TView CreateViewInstance(TFactoryArgs args){
			TView viewInstance = getViewInstance();
			viewInstance.Position = args.Position;
			viewInstance.Forward = args.Forward;
			return viewInstance;
		}

		protected abstract TController CreateController(TView viewInstance, TFactoryArgs args);
	}
}