namespace Core.GameWorld {
	public class WorldObjectLifetimeController {
		private readonly IWorldObjectController worldObjectController;
		private float lifetimeLeft;

		public WorldObjectLifetimeController(IWorldObjectController worldObjectController, float lifetime){
			this.worldObjectController = worldObjectController;
			lifetimeLeft = lifetime;
		}

		public void Update(float dt){
			if ((lifetimeLeft -= dt) <= 0.0f){
				worldObjectController.Dispose();
			}
		}
	}
}