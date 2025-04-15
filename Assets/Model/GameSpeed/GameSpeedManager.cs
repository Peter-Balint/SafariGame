using UnityEngine;

namespace Safari.Model.GameSpeed
{
    public class GameSpeedManager
    {
        public GameSpeed CurrentSpeed { get; set; }

        public GameSpeedManager() 
        {
            CurrentSpeed = GameSpeed.Slow;
        }

        public float CurrentSpeedToFloat()
        {
            switch (CurrentSpeed)
            {
                case GameSpeed.Slow:
                    {
                        return 0.5f;
                    }
                case GameSpeed.Medium:
                    {
                        return 1;
                    }
                case GameSpeed.Fast:
                    {
                        return 5;
                    }
            }
            throw new System.Exception("Invalid game speed");
        }
    }
    public enum GameSpeed { Slow, Medium, Fast } //might rename later
}
