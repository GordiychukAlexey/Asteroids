using System;
using UnityEngine;

namespace Core.GameWorld.Entities.Projectile.Bullet {
	[Serializable]
	public class BulletConfig {
		[SerializeField] private float speed = 12.0f;
		[SerializeField] private float lifetime = 3.0f;
		[SerializeField] private bool isImmortal = false;

		public float Speed => speed;
		public float Lifetime => lifetime;
		public bool IsImmortal => isImmortal;
	}
}