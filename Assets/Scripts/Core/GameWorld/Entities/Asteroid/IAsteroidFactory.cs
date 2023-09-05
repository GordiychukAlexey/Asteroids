using Core.Tools.WorldObjectFactory;

namespace Core.GameWorld.Entities.Asteroid {
	public interface IAsteroidFactory : IWorldObjectFactory<IAsteroidController, IAsteroidFactoryArgs> {
		public IAsteroidController Create(IAsteroidFactoryArgs args);
	}
}