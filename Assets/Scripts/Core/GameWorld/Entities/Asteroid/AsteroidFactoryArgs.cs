using Core.Tools.WorldObjectFactory;
using UnityEngine;

namespace Core.GameWorld.Entities.Asteroid {
	public class AsteroidFactoryArgs : WorldObjectFactoryArgs, IAsteroidFactoryArgs {
		public Vector2 MovingSpeed{ get; }
		public float AngularSpeed{ get; }

		public AsteroidFactoryArgs(
			Vector2 position,
			Vector2 forward,
			Vector2 movingSpeed,
			float angularSpeed
		) : base(position, forward){
			MovingSpeed = movingSpeed;
			AngularSpeed = angularSpeed;
		}
	}
}