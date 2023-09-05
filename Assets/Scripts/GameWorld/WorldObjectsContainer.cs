using System;
using System.Collections.Generic;
using System.Linq;
using Core.GameWorld;
using Core.Tools.WorldObjectFactory;

namespace GameWorld {
	public class WorldObjectsContainer : IDisposable {
		public IEnumerable<IWorldObjectController> WorldObjectControllers =>
			worldObjects
			   .Concat(worldObjectsFactoryEventHandlers
						  .SelectMany(handler => handler.WorldObjectControllers));

		private readonly List<IWorldObjectController> worldObjects = new List<IWorldObjectController>();
		private readonly List<IWorldObjectsFactoryEventHandler> worldObjectsFactoryEventHandlers = new List<IWorldObjectsFactoryEventHandler>();

		public void RegisterWorldObject(IWorldObjectController worldObject) => SubscribeAndAddWorldObject(worldObject);

		private void SubscribeAndAddWorldObject(IWorldObjectController controller){
			controller.OnDispose += UnsubscribeAndRemoveWorldObject;
			worldObjects.Add(controller);
		}

		private void UnsubscribeAndRemoveWorldObject(IWorldObjectController controller){
			controller.OnDispose -= UnsubscribeAndRemoveWorldObject;
			worldObjects.Remove(controller);
		}

		public void RegisterWorldObjectsFactory<TController, TArgs>(IWorldObjectFactory<TController, TArgs> factory)
			where TController : class, IWorldObjectController
			where TArgs : IWorldObjectFactoryArgs{
			var x = new WorldObjectsFactoryEventHandler<IWorldObjectFactory<TController, TArgs>, TController, TArgs>(factory);
			worldObjectsFactoryEventHandlers.Add(x);
		}

		public void Dispose(){
			foreach (IWorldObjectsFactoryEventHandler worldObjectsFactoryEventHandler in worldObjectsFactoryEventHandlers){
				worldObjectsFactoryEventHandler.Dispose();
			}
		}

		private interface IWorldObjectsFactoryEventHandler : IDisposable {
			public IEnumerable<IWorldObjectController> WorldObjectControllers{ get; }
		}

		private class WorldObjectsFactoryEventHandler<TFactory, TController, TArgs> : IWorldObjectsFactoryEventHandler
			where TFactory : IWorldObjectFactory<TController, TArgs>
			where TController : class, IWorldObjectController
			where TArgs : IWorldObjectFactoryArgs {
			public IEnumerable<IWorldObjectController> WorldObjectControllers => worldObjectControllers;

			private readonly List<IWorldObjectController> worldObjectControllers = new List<IWorldObjectController>();

			public WorldObjectsFactoryEventHandler(TFactory factory){
				factory.OnCreateEvent += SubscribeAndAddWorldObject;
			}

			private void SubscribeAndAddWorldObject(TController controller){
				controller.OnDispose += UnsubscribeAndRemoveWorldObject;
				worldObjectControllers.Add(controller);
			}

			private void UnsubscribeAndRemoveWorldObject(IWorldObjectController controller){
				controller.OnDispose -= UnsubscribeAndRemoveWorldObject;
				worldObjectControllers.Remove(controller);
			}

			public void Dispose(){
				var x = worldObjectControllers.ToArray();
				foreach (IWorldObjectController worldObjectController in x){
					UnsubscribeAndRemoveWorldObject(worldObjectController);
				}
			}
		}
	}
}