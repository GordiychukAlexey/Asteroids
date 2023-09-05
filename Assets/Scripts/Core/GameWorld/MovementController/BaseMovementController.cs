namespace Core.GameWorld.MovementController {
	public abstract class BaseMovementController<TConfig> : IMovementController {
		protected readonly IWorldObjectController self;
		protected readonly TConfig config;

		public BaseMovementController(IWorldObjectController self, TConfig config){
			this.self = self;
			this.config = config;
		}

		public abstract void Update(float dt);
	}
}