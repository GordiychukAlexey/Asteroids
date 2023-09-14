using Core;
using Core.GameWorld;
using Core.GameWorld.Entities.Asteroid;
using Core.GameWorld.WorldBoundsProvider;
using Core.Tools.InfinityWorld;
using UnityEngine.Pool;

namespace GameWorld.Entities.Asteroid {
	public class AsteroidFactory : BaseAsteroidFactory<Asteroid, AsteroidFactoryArgs> {
		private readonly AsteroidConfig config;
		private readonly Scorer scorer;
		private readonly IWorldBoundsProvider worldBoundsProvider;
		private readonly IInfinityWorld infinityWorld;

		public AsteroidFactory(
			IObjectPool<Asteroid> viewPool,
			AsteroidConfig config,
			Scorer scorer,
			IWorldBoundsProvider worldBoundsProvider,
			IInfinityWorld infinityWorld)
			: base(viewPool){
			this.config = config;
			this.scorer = scorer;
			this.worldBoundsProvider = worldBoundsProvider;
			this.infinityWorld = infinityWorld;
		}

		protected override IAsteroidController CreateController(Asteroid viewInstance, IAsteroidFactoryArgs args){
			var controller = new AsteroidController(viewInstance, config, worldBoundsProvider, infinityWorld);
			
			scorer.Register(controller);

			return controller;
		}
	}
}