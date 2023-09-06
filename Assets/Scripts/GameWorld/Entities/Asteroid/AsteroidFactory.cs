using Core;
using Core.GameWorld.Entities.Asteroid;
using Core.Tools.ServiceLocator;
using UnityEngine.Pool;

namespace GameWorld.Entities.Asteroid {
	public class AsteroidFactory : BaseAsteroidFactory<Asteroid, AsteroidFactoryArgs> {
		private readonly AsteroidConfig config;
		private readonly Scorer scorer;

		public AsteroidFactory(IObjectPool<Asteroid> viewPool, AsteroidConfig config) : base(viewPool){
			this.config = config;
			scorer = ServiceLocator.Resolve<Scorer>();
		}

		protected override IAsteroidController CreateController(Asteroid viewInstance, IAsteroidFactoryArgs args){
			var controller=new AsteroidController(viewInstance, config);
			
			scorer.Register(controller);

			return controller;
		}
	}
}