using Core.Tools.WorldObjectFactory;
using UnityEngine;

namespace Core.GameWorld.Entities.Projectile.Bullet {
	public class BulletFactoryArgs : WorldObjectFactoryArgs, IBulletFactoryArgs {
		public IWorldObjectController Owner{ get; }
		public Vector2 OwnerMovingSpeed{ get; }
		public float OwnerAngularSpeed{ get; }

		public BulletFactoryArgs(
			Vector2 position,
			Vector2 forward,
			IWorldObjectController owner,
			Vector2 ownerMovingSpeed,
			float ownerAngularSpeed
		) : base(position, forward){
			Owner = owner;
			OwnerMovingSpeed = ownerMovingSpeed;
			OwnerAngularSpeed = ownerAngularSpeed;
		}
	}
}