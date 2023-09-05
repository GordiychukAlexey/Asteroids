using Core.GameWorld;
using Core.GameWorld.Entities.Asteroid;
using Core.GameWorld.Entities.EnemyShip;
using Core.Tools.ServiceLocator;
using Core.Tools.WorldObjectFactory;
using UnityEngine.Pool;

namespace GameWorld.Entities.EnemyShip {
	public class EnemyShipFactory : WorldObjectFactory<IEnemyShipController, EnemyShip, IEnemyShipFactoryArgs> ,  IEnemyShipFactory{
		private readonly EnemyShipConfig config;
		private readonly PlayerShipProvider playerShipProvider;

		public EnemyShipFactory(IObjectPool<EnemyShip> viewPool,EnemyShipConfig config) : base(viewPool){
			this.config = config;
			playerShipProvider = ServiceLocator.Resolve<PlayerShipProvider>();
		}

		protected override IEnemyShipController CreateController(EnemyShip viewInstance, IEnemyShipFactoryArgs args){
			IEnemyShipController controller = new EnemyShipController(viewInstance, config);
			
			controller.SetTarget(playerShipProvider.Provide());

			return controller;
		}
	}
}