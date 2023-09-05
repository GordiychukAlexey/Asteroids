using Core.GameWorld.SpawnLocation;
using Core.Tools.WorldObjectFactory;
using UnityEngine;

namespace Core.GameWorld.EntitiesSpawner {
	public abstract class EntitiesPeriodicalSpawner<TController, TSpawnArgs> : IEntitiesSpawner
		where TController : class, IWorldObjectController
		where TSpawnArgs : IWorldObjectFactoryArgs {
		private readonly ISpawnLocation spawnLocation;
		private readonly IWorldObjectFactory<TController, TSpawnArgs> worldObjectFactory;
		private readonly float spawnDelay;

		private float nextSpawnTime = -1;

		public EntitiesPeriodicalSpawner(ISpawnLocation spawnLocation, IWorldObjectFactory<TController, TSpawnArgs> worldObjectFactory,
										 float spawnRate){
			this.spawnLocation = spawnLocation;
			this.worldObjectFactory = worldObjectFactory;
			spawnDelay = 1.0f / spawnRate;
		}

		public void Update(float dt){
			if (Time.time > nextSpawnTime){
				nextSpawnTime = Time.time + spawnDelay;
				CreateEnemy();
			}
		}

		private void CreateEnemy(){
			var args = GetArgs(spawnLocation.GetNewSpawnPosition());
			var x = worldObjectFactory.Create(args);
		}

		protected abstract TSpawnArgs GetArgs(Vector2 spawnPosition);

		public void Dispose(){ }
	}
}