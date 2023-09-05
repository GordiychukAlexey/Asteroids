using UnityEngine;

public class GameLauncher : MonoBehaviour {
	[SerializeField] private GameController.Config gameControllerConfig;

	private GameController gameController;

	private void Awake(){
		gameController = new GameController(gameControllerConfig);
	}

	private void FixedUpdate(){
		gameController.UpdateGameplay(Time.fixedDeltaTime);
	}
	
	private void Update(){
		gameController.UpdateUI();
	}

	private void OnDestroy(){
		gameController.Dispose();
	}
}