using Core.GameWorld.Entities.PlayerShip;
using Core.Tools.WorldObjectFactory;
using UnityEngine;

namespace GameWorld.Entities.PlayerShip {
	public class PlayerShipFactory : WorldObjectFactory<PlayerShipController, PlayerShip, PlayerShipFactory.Args> {
		public PlayerShipFactory(PlayerShip view) : base(view){ }

		protected override PlayerShipController CreateController(PlayerShip viewInstance, Args args){
			return new PlayerShipController(viewInstance, args.Config);
		}

		public class Args : WorldObjectFactoryArgs {
			public PlayerShipConfig Config{ get; }

			public Args(
				Vector2 position,
				Vector2 forward,
				PlayerShipConfig config
			) : base(position, forward){
				Config = config;
			}
		}
	}
}