using Core.GameWorld.Entities.Asteroid;
using UnityEngine.Pool;

namespace GameWorld.Entities.Asteroid {
	public class AsteroidFactory : BaseAsteroidFactory<Asteroid, AsteroidFactoryArgs> {
		private readonly AsteroidConfig config;

		public AsteroidFactory(IObjectPool<Asteroid> viewPool, AsteroidConfig config) : base(viewPool){
			this.config = config;
		}

		protected override IAsteroidController CreateController(Asteroid viewInstance, IAsteroidFactoryArgs args) =>
			new AsteroidController(viewInstance, config);
	}

//	public class AsteroidFactory : BaseAsteroidFactory<Asteroid, AsteroidFactoryArgs> {
//		private readonly AsteroidConfig config;
//
//		public AsteroidFactory(IObjectPool<Asteroid> viewPool, AsteroidConfig config) : base(viewPool){
//			this.config = config;
//		}
//
//		protected override IAsteroidController CreateController(Asteroid viewInstance, AsteroidFactoryArgs args)=>
//			new AsteroidController(viewInstance, config);
//	}
}