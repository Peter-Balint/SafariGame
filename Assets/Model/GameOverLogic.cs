
using Safari.Model.Animals;
using Safari.Model.Assets;
using Safari.Model.GameSpeed;
using System;




namespace Safari.Model
{
    public class GameOverLogic 
    {
		private AnimalCollection animalCollection;
		private MoneyManager moneyManager;
		private GameSpeedManager speedManager;
		private VisitorManager visitorManager;
		private double winTimerInGameMinutes = 0;
		double winMinutesRequired;
		int animalWinThreshold;
		int moneyWinThreshold;
		int visitorThreshold;
		public bool HasWon = false;
		public bool HasLost = false;

		public GameOverLogic(double winMinutesRequired, int animalWinThreshold, int moneyWinThreshold, int visitorTreshold)
		{
			animalCollection = SafariGame.Instance.Animals;
			moneyManager = SafariGame.Instance.MoneyManager;
			speedManager = SafariGame.Instance.GameSpeedManager;
			visitorManager = SafariGame.Instance.Visitors;
			this.winMinutesRequired = winMinutesRequired;
			this.animalWinThreshold = animalWinThreshold;
			this.moneyWinThreshold = moneyWinThreshold;
			this.visitorThreshold = visitorTreshold;
		}

		/// <summary>
		/// This function is responsible for checking all winning conditions. Delta time is required for measuring the time spent in winning state.
		/// The main if branch uses 3 smaller bool fucntions for the winning criterias.
		/// </summary>
		/// <param name="deltaTime"></param>
		/// <returns></returns>
		public bool CheckWinConditions(float deltaTime)
		{

			if (HasWon) return true;
			if (HasLost) return false;

			if (WinningCriteriaAnimals() && WinningCriteriaMoney() && WinningCriteriaVisitors())
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
		/// <summary>
		/// A helper function for CheckWinConditions, counts if the animals exceed the required number for a win.
		/// </summary>
		/// <returns></returns>
		private bool WinningCriteriaAnimals()
		{
			if (animalCollection.Animals.Count >= animalWinThreshold)
			{
				return true;
			}
			return false;
		}
		/// <summary>
		/// A helper function for CheckWinConditions, counts if the money exceed the required number for a win.
		/// </summary>
		/// <returns></returns>
		private bool WinningCriteriaMoney()
		{
			if (moneyManager.ReadBalance() >= moneyWinThreshold)
			{
				return true;
			}
			return false;
		}
		/// <summary>
		/// A helper function for CheckWinConditions, counts if the visitors exceed the required number for a win.
		/// </summary>
		/// <returns></returns>
		private bool WinningCriteriaVisitors()
		{
			if (visitorManager.ActualVisitors >= visitorThreshold)
			{
				return true;
			}
			return false;
		}
		/// <summary>
		/// Checks if all the money is spent, if yes the game is lost.
		/// </summary>
		/// <returns></returns>
		public bool CheckGameOverByBankruptcy()
		{
			if (moneyManager.ReadBalance() <= 0)
			{
				HasLost = true;
				return true;
			}
			return false;
		}
		/// <summary>
		/// Checks if animals go extinct, if yes the game is lost.
		/// </summary>
		/// <returns></returns>
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
