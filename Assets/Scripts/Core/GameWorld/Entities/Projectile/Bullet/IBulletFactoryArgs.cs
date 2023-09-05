using Core.GameWorld.Entities.Projectile;
using Core.Tools.WorldObjectFactory;
using UnityEngine;

namespace Core.GameWorld.Entities.Bullet {
	public interface IBulletFactoryArgs : IProjectileFactoryArgs {
		public Vector2 OwnerMovingSpeed{ get; }
		public float OwnerAngularSpeed{ get; }
	}
}