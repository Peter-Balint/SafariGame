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

        public int CurrentSpeedToNum() //the actual meaning behind the enum
        {
            switch (CurrentSpeed)
            {
                case GameSpeed.Slow:
                    {
                        return 1;
                    }
                case GameSpeed.Medium:
                    {
                        return 3;
                    }
                case GameSpeed.Fast:
                    {
                        return 10;
                    }
            }
            throw new System.Exception("Invalid game speed");
        }
    }
    public enum GameSpeed { Slow, Medium, Fast } //might rename later
}
