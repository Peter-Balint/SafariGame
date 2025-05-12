using Safari.Model;
using Safari.Model.Animals;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Safari.Model.GameSpeed;


namespace Safari.View.UI
{
    public class GameOverManager : MonoBehaviour
    {
	
		private Image panelImage;
		private GameOverLogic gameOverLogic;

		public GameObject GameOverPanel;
		public TMP_Text GameOverMsg;
		public DifficultySettings difficultySettings;
		public Button ContinueButton;
		
		DifficultySettings.DifficultyData settings;


		void Start()
		{
			settings = difficultySettings.GetSettings(SafariGame.Difficulty);

			gameOverLogic = new GameOverLogic(settings.winMinutesRequired, settings.animalWinThreshold, settings.moneyWinThreshold, settings.visitorWinThreshold);

			if (GameOverPanel != null)
			{
				panelImage = GameOverPanel.GetComponent<Image>();
			}
		}
		void Update()
		{
			if (gameOverLogic.HasWon) return;
			if(gameOverLogic.CheckGameOverByBankruptcy())
			{
				GameOverPanel.SetActive(true);
				GameOverMsg.text = "Bankruptcy";
			}
			if(gameOverLogic.CheckGameOverByExtinction())
			{
				GameOverPanel.SetActive(true);
				GameOverMsg.text = "Extinction";
			}
			if (gameOverLogic.CheckWinConditions(Time.deltaTime))
			{
				TriggerWin();
			}


		}

		public void ReturnToMainMenu()
		{
			SceneManager.LoadScene("MainMenu");
		}

		public void OnContinue()
		{
			GameOverPanel.SetActive(false);

		}
		private void TriggerWin()
		{
			GameOverPanel.SetActive(true);
			Color color = Color.green;
			color.a = 0.6f;
			panelImage.color = color;
			GameOverMsg.text = "You Won!";
			if (ContinueButton != null)
				ContinueButton.gameObject.SetActive(true);
		}

		

	



	}
}
