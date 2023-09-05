using Core.GameWorld.Entities.Asteroid;
using Core.Tools.WorldObjectFactory;
using UnityEngine;

namespace Core.GameWorld.Entities.EnemyShip {
	public class EnemyShipFactoryArgs : WorldObjectFactoryArgs, IEnemyShipFactoryArgs {
//			public EnemyShipConfig Config{ get; }

		public EnemyShipFactoryArgs(
			Vector2 position,
			Vector2 forward //,
//				EnemyShipConfig config
		) : base(position, forward){
//				Config = config;
		}
	}
}