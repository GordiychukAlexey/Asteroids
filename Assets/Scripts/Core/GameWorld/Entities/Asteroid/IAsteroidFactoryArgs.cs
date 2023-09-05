using Core.Tools.WorldObjectFactory;
using UnityEngine;

namespace Core.GameWorld.Entities.Asteroid {
	public interface IAsteroidFactoryArgs : IWorldObjectFactoryArgs {
		public Vector2 MovingSpeed{ get; }
		public float AngularSpeed{ get; }
	}
}