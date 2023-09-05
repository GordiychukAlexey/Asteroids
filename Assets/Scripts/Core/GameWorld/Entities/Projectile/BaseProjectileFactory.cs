using Core.Tools.WorldObjectFactory;
using UnityEngine;
using UnityEngine.Pool;

namespace Core.GameWorld.Entities.Projectile {
	public abstract class BaseProjectileFactory<TController, TView, TArgs> :
		WorldObjectFactory<IProjectileController, TView, IProjectileFactoryArgs>, IProjectileFactory
		where TController : class, IProjectileController
		where TView : Object, IProjectileView
		where TArgs : IProjectileFactoryArgs {
		public BaseProjectileFactory(IObjectPool<TView> viewPool) : base(viewPool){ }
	}
}