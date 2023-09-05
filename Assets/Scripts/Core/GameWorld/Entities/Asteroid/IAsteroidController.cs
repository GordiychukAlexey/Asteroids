using UnityEngine;

namespace Core.GameWorld.Entities.Asteroid {
	public interface IAsteroidController : IWorldObjectController {
		public void SetForward(Vector2 value);
		public void SetSpeed(Vector2 value);
		public void SetAngularSpeed(float value);
		public void Update(float dt);
	}
}