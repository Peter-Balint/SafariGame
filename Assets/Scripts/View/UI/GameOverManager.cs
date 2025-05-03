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
		private AnimalCollection animalCollection;
		private MoneyManager moneyManager;
		private GameSpeedManager speedManager;
		private Image panelImage;
		private bool hasWon = false;
		private double winTimerInGameMinutes = 0;

		public GameObject GameOverPanel;
		//public event EventHandler Bankruptcy;
		//public event EventHandler Extinction;

		public TMP_Text GameOverMsg;
		public DifficultySettings difficultySettings;
		public Button ContinueButton;
		DifficultySettings.DifficultyData settings;




		void Start()
		{
			animalCollection = SafariGame.Instance.Animals;
			moneyManager = SafariGame.Instance.MoneyManager;
			speedManager = SafariGame.Instance.GameSpeedManager;

			settings = difficultySettings.GetSettings(SafariGame.Difficulty);



			if (GameOverPanel != null)
			{
				panelImage = GameOverPanel.GetComponent<Image>();
			}
		}
		void Update()
		{
			CheckGameOverByBankruptcy();
			CheckGameOverByExtinction();
			CheckWinConditions();


		}

		public bool CheckGameOverByBankruptcy()
		{
			if(moneyManager.ReadBalance() <= 0)
			{
				
				GameOverPanel.SetActive(true);
				GameOverMsg.text = "Bankruptcy";
				return true;

			}
			return false;
		}

		public bool CheckGameOverByExtinction()
		{
			if (animalCollection.Animals.Count <= 0)
			{
				GameOverPanel.SetActive(true);
				GameOverMsg.text = "Extinction";
				
				return true;
			}
			return false;
		}

		public void ReturnToMainMenu()
		{
			SceneManager.LoadScene("MainMenu");
		}

		public void OnContinue()
		{
			GameOverPanel.SetActive(false);

		}
		private void CheckWinConditions()
		{
			
			if (hasWon) return;

			if (WinningCriteriaAnimals() && WinningCriteriaMoney())
			{
				winTimerInGameMinutes += speedManager.CurrentSpeedToNum() * Time.deltaTime * 10;
				if (winTimerInGameMinutes >= settings.winMinutesRequired)
				{
					TriggerWin();
					hasWon = true;
				}
			}
			else {
				
				winTimerInGameMinutes = 0;
			}
		}

		private bool WinningCriteriaAnimals()
		{
			if(animalCollection.Animals.Count >= settings.animalWinThreshold) 
			{
				return true;
			}
			return false;
		}

		private bool WinningCriteriaMoney()
		{
			if (moneyManager.ReadBalance() >= settings.moneyWinThreshold) 
			{
				return true;
			}
			return false;
		}

		private bool WinningCriteriaVisitors()
		{
			throw new NotImplementedException();
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
