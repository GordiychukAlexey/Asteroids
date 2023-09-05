using UnityEngine;

namespace Core.GameWorld.Entities.PlayerShip {
	public interface IPlayerShipController : IWorldObjectController {
		public void SetForwardSpeedThrottle(float value);
		public void SetAngularSpeedThrottle(float value);
		public bool Gun1IsSpawnActive{ get; set; }
		public bool Gun2IsSpawnActive{ get; set; }
		
		public Vector2 Speed{get;}
		public float AngularSpeed{get;}
		public int LaserCharges{get;}
		public float LaserChargeTimeLeft{get;}
	}
}