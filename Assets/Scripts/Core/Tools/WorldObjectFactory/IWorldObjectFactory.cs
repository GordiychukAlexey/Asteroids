using System;
using Core.GameWorld;

namespace Core.Tools.WorldObjectFactory {
//	public interface IWorldObjectFactory 
//	{
//		event Action<IWorldObjectController> OnCreateEvent;
//		IWorldObjectController Create(IWorldObjectFactoryArgs args);
//	}

	public interface IWorldObjectFactory<TController, TArgs> //:IWorldObjectFactory
		where TController : class, IWorldObjectController
		where TArgs : IWorldObjectFactoryArgs {
		public event Action<TController> OnCreateEvent;
		public TController Create(TArgs args);
	}
}