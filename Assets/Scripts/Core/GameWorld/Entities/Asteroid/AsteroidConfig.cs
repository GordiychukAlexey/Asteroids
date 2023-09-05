using System;
using UnityEngine;

namespace Core.GameWorld.Entities.Asteroid {
	[Serializable]
	public class AsteroidConfig {
		[SerializeField] private float movementMaxSpeed = 0.5f;
		public IAsteroidFactory AsteroidFactory{ get; set; } = null;

		public float MovementMaxSpeed => movementMaxSpeed;
	}
}