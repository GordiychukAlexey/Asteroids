using UnityEngine;

namespace Core.GameWorld.Entities.Projectile.Bullet {
	public interface IBulletController : IProjectileController {
		public void AddSpeed(Vector2 speed);
		public void SetAngularSpeed(float speed);
	}
}