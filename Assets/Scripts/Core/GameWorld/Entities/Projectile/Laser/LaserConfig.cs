using System;
using UnityEngine;

namespace Core.GameWorld.Entities.Projectile.Laser {
	[Serializable]
	public class LaserConfig {
		[SerializeField] private float lifetime = 0.5f;

		public float Lifetime => lifetime;
	}
}