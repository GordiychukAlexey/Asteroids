using System;
using Core.GameWorld;

namespace Core.Tools.WorldObjectFactory {
	public interface IWorldObjectFactory<TController, TArgs>
		where TController : class, IWorldObjectController
		where TArgs : IWorldObjectFactoryArgs {
		public event Action<TController> OnCreateEvent;
		public TController Create(TArgs args);
	}
}