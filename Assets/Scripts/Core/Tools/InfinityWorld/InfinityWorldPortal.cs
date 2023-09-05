using Core.GameWorld;
using UnityEngine;

namespace Core.Tools.InfinityWorld {
	public class InfinityWorldPortal {
		private readonly IWorldObjectController worldObjectController;
		private readonly InfinityWorld infinityWorld;
		private readonly Bounds worldBounds;

		public bool IsWasInWorldBounds{ get; private set; } = false; //for spawn outside bounds

		public InfinityWorldPortal(IWorldObjectController worldObjectController){
			this.worldObjectController = worldObjectController;

			worldBounds = ServiceLocator.ServiceLocator.Resolve<WorldBoundsProvider>().Bounds;

			infinityWorld = ServiceLocator.ServiceLocator.Resolve<InfinityWorld>();
		}

		public void Update(float dt){
			if (!IsWasInWorldBounds){
				if (worldBounds.Contains(worldObjectController.Position)){
					IsWasInWorldBounds = true;
				}
			}

			if (IsWasInWorldBounds){
				infinityWorld.AdaptPosition(worldObjectController);
			}
		}
	}
}