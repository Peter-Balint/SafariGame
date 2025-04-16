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

        public TMP_Text Clock;

        private GameSpeedManager manager;

        void Start()
        {
            manager = SafariGame.Instance.GameSpeedManager;
            SlowButton.onClick.AddListener(() => manager.CurrentSpeed = GameSpeed.Slow);
            MediumButton.onClick.AddListener(() => manager.CurrentSpeed = GameSpeed.Medium);
            FastButton.onClick.AddListener(() => manager.CurrentSpeed = GameSpeed.Fast);

            Clock.text = manager.Time.ToString("MM/dd/yyyy HH:mm");

            manager.DayPassed += ((sender, args) => Debug.Log("a day has passed"));
        }

        void Update()
        {
            manager.AddTime(Time.deltaTime);
            Clock.text = manager.Time.ToString("MM/dd/yyyy HH:mm");
        }

        
    }

}
