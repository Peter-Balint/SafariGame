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

        private DateTime time;

        private GameSpeedManager manager;

        void Start()
        {
            manager = SafariGame.Instance.GameSpeedManager;
            SlowButton.onClick.AddListener(() => manager.CurrentSpeed = GameSpeed.Slow);
            MediumButton.onClick.AddListener(() => manager.CurrentSpeed = GameSpeed.Medium);
            FastButton.onClick.AddListener(() => manager.CurrentSpeed = GameSpeed.Fast);

            time = DateTime.Now;
            Clock.text = time.ToString("MM/dd/yyyy HH:mm");
        }

        void Update()
        {
            float delta = Time.deltaTime;
            time = time.AddMinutes(manager.CurrentSpeedToNum() * delta * 5);
            Clock.text = time.ToString("MM/dd/yyyy HH:mm");
        }

        
    }

}
