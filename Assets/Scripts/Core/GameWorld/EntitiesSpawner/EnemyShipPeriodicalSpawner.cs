using Core.GameWorld.Entities.EnemyShip;
using Core.GameWorld.SpawnLocation;
using UnityEngine;

namespace Core.GameWorld.EntitiesSpawner {
	public class EnemyShipPeriodicalSpawner : EntitiesPeriodicalSpawner<IEnemyShipController, IEnemyShipFactoryArgs> {
		private readonly PlayerShipProvider playerShipProvider;

		public EnemyShipPeriodicalSpawner(
			ISpawnLocation spawnLocation,
			IEnemyShipFactory worldObjectFactory,
			float spawnRate,
			PlayerShipProvider playerShipProvider)
			: base(spawnLocation, worldObjectFactory, spawnRate){
			this.playerShipProvider = playerShipProvider;
		}

		protected override IEnemyShipFactoryArgs GetArgs(Vector2 spawnPosition){
			return new EnemyShipFactoryArgs(
				spawnPosition,
				playerShipProvider.Provide().Position - spawnPosition);
		}
	}
}