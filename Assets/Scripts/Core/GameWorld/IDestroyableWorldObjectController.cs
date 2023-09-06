using System;
using Core.GameWorld.Entities.Projectile;

namespace Core.GameWorld {
	public interface IDestroyableWorldObjectController : IWorldObjectController {
		public event Action<IDestroyableWorldObjectController> OnDestroy;
	}
}