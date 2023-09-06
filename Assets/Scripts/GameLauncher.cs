using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLauncher : MonoBehaviour {
	[SerializeField] private GameController.Config gameControllerConfig;

	private GameController gameController;

	private void Awake(){
		Time.timeScale = 1.0f;
		gameController = new GameController(gameControllerConfig);

		gameController.OnRestart += Restart;
	}

	private void FixedUpdate(){
		gameController.UpdateGameplay(Time.fixedDeltaTime);
	}

	private void Update(){
		gameController.UpdateUI();
	}

	private void Restart(){
		gameController.Dispose();

		SceneManager.LoadScene(0);
	}

	private void OnDestroy(){
		gameController.OnRestart -= Restart;

		gameController.Dispose();
	}
}