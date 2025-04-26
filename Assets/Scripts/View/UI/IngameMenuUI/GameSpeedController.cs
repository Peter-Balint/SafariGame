using UnityEngine;
using UnityEngine.UI;
using Safari.Model.GameSpeed;
using Safari.Model;
using System;
using TMPro;

namespace Safari.View.UI
{
    public class GameSpeedController : MonoBehaviour
    {
        public Button SlowButton;
        public Button MediumButton;
        public Button FastButton;


        MoneyManager moneyManager;
        int VisitorSpawnedSec = 0;

        public TMP_Text Clock;

        private GameSpeedManager manager;
		System.Random r = new System.Random();


		void Start()
        {
            manager = SafariGame.Instance.GameSpeedManager;
            moneyManager = SafariGame.Instance.MoneyManager;
            SlowButton.onClick.AddListener(() => manager.CurrentSpeed = GameSpeed.Slow);
            MediumButton.onClick.AddListener(() => manager.CurrentSpeed = GameSpeed.Medium);
            FastButton.onClick.AddListener(() => manager.CurrentSpeed = GameSpeed.Fast);

            Clock.text = manager.Time.ToString("MM/dd/yyyy HH:mm");

            manager.DayPassed += ((sender, args) => Debug.Log("a day has passed"));


		}

		void Update()
        {
            if((int)manager.minutesToday % 10 == 0 && VisitorSpawnedSec != (int)manager.minutesToday)
            {
                moneyManager.CalculateVisitDesire();
                VisitorSpawn();
                VisitorSpawnedSec = (int)manager.minutesToday;
            }
		


			manager.AddTime(Time.deltaTime);
            Clock.text = manager.Time.ToString("MM/dd/yyyy HH:mm");
            
           
        }

		private void VisitorSpawn()
		{
			double random = r.Next(0, 10);
			random *= 0.1;

			if (moneyManager.ReadVisitDesire() > random)
			{
				moneyManager.AddToBalance(moneyManager.ReadTicketPrice());
			}
		}



	}

}
