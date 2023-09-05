using Core.Tools.WorldObjectFactory;
using UnityEngine;

namespace Core.GameWorld.Entities.Bullet.PlayerBullet {
	public class BulletFactoryArgs : WorldObjectFactoryArgs, IBulletFactoryArgs {
		public Vector2 OwnerMovingSpeed{ get; }
		public float OwnerAngularSpeed{ get; }

		public BulletFactoryArgs(
			Vector2 position,
			Vector2 forward,
			Vector2 ownerMovingSpeed,
			float ownerAngularSpeed
		) : base(position, forward){
			OwnerMovingSpeed = ownerMovingSpeed;
			OwnerAngularSpeed = ownerAngularSpeed;
		}
	}
}