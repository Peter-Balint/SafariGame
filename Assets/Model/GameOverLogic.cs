
using Safari.Model.Animals;
using Safari.Model.GameSpeed;
using System;




namespace Safari.Model
{
    public class GameOverLogic 
    {
		private AnimalCollection animalCollection;
		private MoneyManager moneyManager;
		private GameSpeedManager speedManager;
		private double winTimerInGameMinutes = 0;
		double winMinutesRequired;
		int animalWinThreshold;
		int moneyWinThreshold;
		public bool HasWon = false;
		public bool HasLost = false;

		public GameOverLogic(double winMinutesRequired, int animalWinThreshold, int moneyWinThreshold)
		{
			animalCollection = SafariGame.Instance.Animals;
			moneyManager = SafariGame.Instance.MoneyManager;
			speedManager = SafariGame.Instance.GameSpeedManager;
			this.winMinutesRequired = winMinutesRequired;
			this.animalWinThreshold = animalWinThreshold;
			this.moneyWinThreshold = moneyWinThreshold;
		}

		public bool CheckWinConditions(float deltaTime)
		{

			if (HasWon) return true;
			if (HasLost) return false;

			if (WinningCriteriaAnimals() && WinningCriteriaMoney())
			{
				winTimerInGameMinutes += speedManager.CurrentSpeedToNum()/60 * deltaTime;
				if (winTimerInGameMinutes >= winMinutesRequired)
				{
					HasWon = true;
					return true;
				}
			}
			else
			{

				winTimerInGameMinutes = 0;
				return false;
			}
			return false;
		}
		private bool WinningCriteriaAnimals()
		{
			if (animalCollection.Animals.Count >= animalWinThreshold)
			{
				return true;
			}
			return false;
		}

		private bool WinningCriteriaMoney()
		{
			if (moneyManager.ReadBalance() >= moneyWinThreshold)
			{
				return true;
			}
			return false;
		}
		private bool WinningCriteriaVisitors()
		{
			throw new NotImplementedException();
		}

		public bool CheckGameOverByBankruptcy()
		{
			if (moneyManager.ReadBalance() <= 0)
			{
				HasLost = true;
				return true;
			}
			return false;
		}

		public bool CheckGameOverByExtinction()
		{
			if (animalCollection.Animals.Count <= 0)
			{
				HasLost = true;
				return true;
			}
			return false;
		}

	}
}
