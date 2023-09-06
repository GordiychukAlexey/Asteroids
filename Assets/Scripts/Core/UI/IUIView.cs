using System;

namespace Core.UI {
	public interface IUIView {
		public event Action OnRestartPressed;
		public void SetStats(GameplayStats stats);
		public void ShowGameOverScreen(int score);
	}
}