using System;
using Core.GameWorld.Entities.Projectile;
using UnityEngine;

namespace Core.GameWorld.Entities.Bullet {
	public interface IBulletController : IProjectileController {
//		public event Action<IBulletController> OnDispose;
		void AddSpeed(Vector2 speed);

		void SetAngularSpeed(float speed);
//		public void Update(float dt);
	}
}