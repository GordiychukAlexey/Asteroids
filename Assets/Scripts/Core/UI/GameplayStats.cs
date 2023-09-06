using UnityEngine;

namespace Core.UI {
	public struct GameplayStats {
		public readonly Vector2 PlayerShipPosition;
		public readonly float PlayerShipAngle;
		public readonly float PlayerShipSpeed;
		public readonly int LaserChargesCount;
		public readonly float LaserChargesTimer;
		public readonly int Score;

		public GameplayStats(Vector2 playerShipPosition,
							 float playerShipAngle, 
							 float playerShipSpeed,
							 int laserChargesCount, 
							 float laserChargesTimer,
							 int score){
			PlayerShipPosition = playerShipPosition;
			PlayerShipAngle = playerShipAngle;
			PlayerShipSpeed = playerShipSpeed;
			LaserChargesCount = laserChargesCount;
			LaserChargesTimer = laserChargesTimer;
			Score = score;
		}
	}
}