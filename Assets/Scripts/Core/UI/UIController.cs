using System;

namespace Core.UI {
	public class UIController : IDisposable {
		public event Action onRestartPressed;

		private readonly IUIView view;

		private GameplayStats lastStats;

		public UIController(IUIView view){
			this.view = view;

			view.OnRestartPressed += RestartHandler;
		}

		private void RestartHandler(){
			onRestartPressed?.Invoke();
		}

		public void SetStats(GameplayStats gameplayStats){
			lastStats = gameplayStats;

			view.SetStats(gameplayStats);
		}

		public void ShowGameOver(){
			view.ShowGameOverScreen(lastStats.Score);
		}

		public void Dispose(){
			view.OnRestartPressed -= RestartHandler;
		}
	}
}