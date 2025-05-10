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
            // Not the best place to put this, might refactor if we find a better place
            SafariGame.Instance.Visitors.Update();
            manager.AddTime(Time.deltaTime);
            Clock.text = manager.Time.ToString("MM/dd/yyyy HH:mm");
        }
	}

}
