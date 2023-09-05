using Core.Tools.WorldObjectFactory;
using UnityEngine;
using UnityEngine.Pool;

namespace Core.GameWorld.Entities.Asteroid {
	public abstract class BaseAsteroidFactory<TView, TArgs> :
		WorldObjectFactory<IAsteroidController, TView, IAsteroidFactoryArgs>, IAsteroidFactory
		where TView : Object, IAsteroidView
//		where TArgs : IAsteroidFactoryArgs
	{
		public BaseAsteroidFactory(IObjectPool<TView> viewPool) : base(viewPool){ }

		public IAsteroidController Create(IAsteroidFactoryArgs args){
			var controller = base.Create(args);
			controller.SetSpeed(args.MovingSpeed);
			controller.SetAngularSpeed(args.AngularSpeed);

			return controller;
		}
	}

//	public abstract class BaseAsteroidFactory<TView, TArgs> :
//		WorldObjectFactory<IAsteroidController, TView, TArgs>, IAsteroidFactory
//		where TView : Object, IAsteroidView
//		where TArgs : IAsteroidFactoryArgs
//	{
//		public BaseAsteroidFactory(IObjectPool<TView> viewPool) : base(viewPool){ }
//
//		public IAsteroidController Create(IAsteroidFactoryArgs args){
//			var controller = base.Create((TArgs)args);
//			controller.SetSpeed(args.MovingSpeed);
//			controller.SetAngularSpeed(args.AngularSpeed);
//
//			return controller;
//		}
//	}
}