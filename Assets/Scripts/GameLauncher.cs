using UnityEngine;

public class GameLauncher : MonoBehaviour {
	[SerializeField] private GameController.Config gameControllerConfig;

	private GameController gameController;

	private void Awake(){
		gameController = new GameController(gameControllerConfig);
	}

	private void FixedUpdate(){
		gameController.Update(Time.fixedDeltaTime);
	}

	private void OnDestroy(){
		gameController.Dispose();
	}
}