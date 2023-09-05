using Core.GameWorld.Entities.PlayerShip;
using Core.Tools.InfinityWorld;
using Core.Tools.WorldObjectFactory;
using UnityEngine;

namespace Core.GameWorld.Entities.Bullet.PlayerBullet {
	public class LaserFactoryArgs : WorldObjectFactoryArgs, ILaserFactoryArgs {
		public IWorldObjectController Owner{ get; }

		public LaserFactoryArgs(
			Vector2 position,
			Vector2 forward,
			IWorldObjectController owner) : base(position, forward){
			Owner = owner;
		}
	}
}