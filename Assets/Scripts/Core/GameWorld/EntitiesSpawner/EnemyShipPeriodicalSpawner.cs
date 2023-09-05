using Core.GameWorld.Entities.EnemyShip;
using Core.GameWorld.SpawnLocation;
using Core.Tools.ServiceLocator;
using UnityEngine;

namespace Core.GameWorld.EntitiesSpawner {
	public class EnemyShipPeriodicalSpawner : EntitiesPeriodicalSpawner<IEnemyShipController, IEnemyShipFactoryArgs> {
		private readonly PlayerShipProvider playerShipProvider;

		public EnemyShipPeriodicalSpawner(
			ISpawnLocation spawnLocation,
			IEnemyShipFactory worldObjectFactory,
			float spawnRate)
			: base(spawnLocation, worldObjectFactory, spawnRate){
			playerShipProvider = ServiceLocator.Resolve<PlayerShipProvider>();
		}

		protected override IEnemyShipFactoryArgs GetArgs(Vector2 spawnPosition){
			return new EnemyShipFactoryArgs(
				spawnPosition,
				playerShipProvider.Provide().Position - spawnPosition);
		}
	}
}