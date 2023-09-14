using Core;
using Core.GameWorld;
using Core.GameWorld.Entities.EnemyShip;
using Core.GameWorld.WorldBoundsProvider;
using Core.Tools.InfinityWorld;
using Core.Tools.WorldObjectFactory;
using UnityEngine.Pool;

namespace GameWorld.Entities.EnemyShip {
	public class EnemyShipFactory : WorldObjectFactory<IEnemyShipController, EnemyShip, IEnemyShipFactoryArgs>, IEnemyShipFactory {
		private readonly EnemyShipConfig config;
		private readonly PlayerShipProvider playerShipProvider;
		private readonly Scorer scorer;
		private readonly IWorldBoundsProvider worldBoundsProvider;
		private readonly IInfinityWorld infinityWorld;

		public EnemyShipFactory(
			IObjectPool<EnemyShip> viewPool, 
			EnemyShipConfig config,
			PlayerShipProvider playerShipProvider,
			Scorer scorer,
			IWorldBoundsProvider worldBoundsProvider,
			IInfinityWorld infinityWorld) 
			: base(viewPool){
			this.config = config;
			this.playerShipProvider = playerShipProvider;
			this.scorer = scorer;
			this.worldBoundsProvider = worldBoundsProvider;
			this.infinityWorld = infinityWorld;
		}

		protected override IEnemyShipController CreateController(EnemyShip viewInstance, IEnemyShipFactoryArgs args){
			IEnemyShipController controller = new EnemyShipController(viewInstance, config, worldBoundsProvider, infinityWorld);

			controller.SetTarget(playerShipProvider.Provide());

			scorer.Register(controller);
			
			return controller;
		}
	}
}