using System;

namespace Core.GameWorld.EntitiesSpawner {
	public interface IEntitiesSpawner : IDisposable {
		public void Update(float dt);
	}
}