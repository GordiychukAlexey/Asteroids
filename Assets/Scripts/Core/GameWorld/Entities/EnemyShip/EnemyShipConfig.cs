using System;
using UnityEngine;

namespace Core.GameWorld.Entities.EnemyShip {
	[Serializable]
	public class EnemyShipConfig {
		[SerializeField] private float speed = 1.0f;
		[SerializeField] private float maxRotateSpeed = 90.0f;

		public float Speed => speed;
		public float MaxRotateSpeed => maxRotateSpeed;
	}
}