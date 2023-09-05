using Core.Tools.WorldObjectFactory;
using UnityEngine;

namespace Core.GameWorld.Entities.EnemyShip {
	public class EnemyShipFactoryArgs : WorldObjectFactoryArgs, IEnemyShipFactoryArgs {
		public EnemyShipFactoryArgs(
			Vector2 position,
			Vector2 forward
		) : base(position, forward){ }
	}
}