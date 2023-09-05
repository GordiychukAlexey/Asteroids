using Core.GameWorld.MovementController;
using UnityEngine;

namespace Core.GameWorld.Entities.Asteroid {
	public class AsteroidController : BaseWorldObjectController<IAsteroidView>, IAsteroidController {
		private readonly AsteroidConfig asteroidConfig;

		private readonly PhysicMovementController movementController;

		public AsteroidController(IAsteroidView view, AsteroidConfig asteroidConfig
		) : base(view){
			this.asteroidConfig = asteroidConfig;

			movementController = new PhysicMovementController(
				this,
				new PhysicMovementController.Config(0.0f, 0.0f));

			view.OnTriggerEnter += TriggerEnterHandler;
		}

		private void TriggerEnterHandler(object sender, Collider2D e){
			Dispose();
		}

		public void SetForward(Vector2 value) => Forward = value;

		public void SetSpeed(Vector2 value) => movementController.SetSpeed(value);

		public void SetAngularSpeed(float value) => movementController.SetAngularSpeed(value);

		public override void Update(float dt){
			movementController.Update(dt);

			base.Update(dt);
		}

		public override void Dispose(){
			view.OnTriggerEnter -= TriggerEnterHandler;

			if (asteroidConfig.AsteroidFactory != null){
				for (int i = 0; i < 2; i++){ //todo
					asteroidConfig.AsteroidFactory.Create(
						new AsteroidFactoryArgs(
							Position,
							Random.insideUnitCircle,
							movementController.Speed + Random.insideUnitCircle * movementController.Speed * 2.0f,
							movementController.AngularSpeed * 2.0f));
				}
			}

			base.Dispose();
		}
	}
}