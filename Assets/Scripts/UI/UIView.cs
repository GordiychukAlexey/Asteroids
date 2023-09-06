using System;
using Core.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
	public class UIView : MonoBehaviour, IUIView {
		public event Action OnRestartPressed;
		
		[SerializeField] private TMP_Text statsText;
		[SerializeField] private Button restartButton;
		[SerializeField] private Transform inGamePanel;
		[SerializeField] private Transform restartPanel;
		[SerializeField] private TMP_Text gameOverScoreText;

		private void Awake(){
			restartButton.onClick.AddListener(RestartHandler);
		}

		private void RestartHandler() => OnRestartPressed?.Invoke();

		public void SetStats(GameplayStats stats){
			statsText.text = $"PlayerShipPosition: {stats.PlayerShipPosition}\n"
						  + $"PlayerShipAngle: {stats.PlayerShipAngle}\n"
						  + $"PlayerShipSpeed: {stats.PlayerShipSpeed}\n"
						  + $"LaserChargesCount: {stats.LaserChargesCount}\n"
						  + $"LaserChargesTimer: {stats.LaserChargesTimer}\n"
						  + $"Score: {stats.Score}\n";
		}

		public void ShowGameOverScreen(int score){
			inGamePanel.gameObject.SetActive(false);
			
			gameOverScoreText.text = $"Score: {score}";
			restartPanel.gameObject.SetActive(true);
		}

		private void OnDestroy(){
			restartButton.onClick.RemoveAllListeners();
		}
	}
}