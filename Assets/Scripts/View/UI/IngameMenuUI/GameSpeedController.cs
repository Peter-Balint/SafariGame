using UnityEngine;
using UnityEngine.UI;
using Safari.Model.GameSpeed;
using Safari.Model;

namespace Safari.View.UI
{
    public class GameSpeedController : MonoBehaviour
    {
        public Button SlowButton;
        public Button MediumButton;
        public Button FastButton;

        private GameSpeedManager manager;

        void Start()
        {
            manager = SafariGame.Instance.GameSpeedManager;
            SlowButton.onClick.AddListener(() => manager.CurrentSpeed = GameSpeed.Slow);
            MediumButton.onClick.AddListener(() => manager.CurrentSpeed = GameSpeed.Medium);
            FastButton.onClick.AddListener(() => manager.CurrentSpeed = GameSpeed.Fast);
        }

        void Update()
        {

        }

        
    }

}
