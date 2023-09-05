using UnityEngine;
using Random = UnityEngine.Random;

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

	//todo testdel
	private void OnGUI(){
		if (GUILayout.Button("Pool spawn")){
			var amount = Random.Range(1, 3);
			for (int i = 0; i < amount; ++i){
				gameController.CreateEnemy(Random.insideUnitCircle * 10, Vector2.up);
			}
		}
	}
}