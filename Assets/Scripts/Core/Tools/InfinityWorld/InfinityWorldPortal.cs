using Core.GameWorld;
using Core.GameWorld.WorldBoundsProvider;
using UnityEngine;

namespace Core.Tools.InfinityWorld {
	public class InfinityWorldPortal {
		private readonly IWorldObjectController worldObjectController;
		private readonly IInfinityWorld infinityWorld;
		private readonly Bounds worldBounds;

		public bool IsWasInWorldBounds{ get; private set; } = false; //for spawn outside bounds

		public InfinityWorldPortal(IWorldObjectController worldObjectController,
			IWorldBoundsProvider worldBoundsProvider,
			IInfinityWorld infinityWorld){
			this.worldObjectController = worldObjectController;

			worldBounds = worldBoundsProvider.Bounds;

			this.infinityWorld = infinityWorld;
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