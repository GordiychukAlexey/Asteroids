using Core.GameWorld.Entities.Asteroid;
using Core.Tools.InfinityWorld;
using Core.Tools.ServiceLocator;
using UnityEngine;

namespace Core.GameWorld.EntitiesSpawner {
	public class AsteroidPeriodicalSpawner : EntitiesPeriodicalSpawner<IAsteroidController, IAsteroidFactoryArgs> {
		private readonly WorldBoundsProvider worldBoundsProvider;
		private readonly float maxMovingSpeed;
		private readonly float maxAngularSpeed;

		public AsteroidPeriodicalSpawner(
			ISpawnLocation spawnLocation,
			IAsteroidFactory worldObjectFactory,
			float spawnRate,
			float maxMovingSpeed,
			float maxAngularSpeed
		)
			: base(spawnLocation, worldObjectFactory, spawnRate){
			worldBoundsProvider = ServiceLocator.Resolve<WorldBoundsProvider>();
			this.maxMovingSpeed = maxMovingSpeed;
			this.maxAngularSpeed = maxAngularSpeed;
		}

		protected override IAsteroidFactoryArgs GetArgs(Vector2 spawnPosition){
			return new AsteroidFactoryArgs(
				spawnPosition,
				Random.insideUnitCircle,
				((Vector2) worldBoundsProvider.Bounds.center
			   + new Vector2( //Направление в случайную точку внутри worldBounds
					 (Random.value * 2.0f - 1.0f) * worldBoundsProvider.Bounds.size.x * 0.8f * 0.5f,
					 (Random.value * 2.0f - 1.0f) * worldBoundsProvider.Bounds.size.y * 0.8f * 0.5f)
			   - spawnPosition).normalized
			  * (Random.value * maxMovingSpeed * 0.9f + maxMovingSpeed * 0.1f),
				Random.value * maxAngularSpeed * 0.9f + maxAngularSpeed * 0.1f);
		}
	}
}