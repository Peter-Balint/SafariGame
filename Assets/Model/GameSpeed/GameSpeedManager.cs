using Safari.Model.Rangers;
using System;
using UnityEngine;

namespace Safari.Model.GameSpeed
{
    public class GameSpeedManager
    {
        public GameSpeed CurrentSpeed { get; set; }
        public DateTime Time { get; set; }

        private const int minutesInDay = 24 * 60;
        public double minutesToday;


        public event EventHandler DayPassed;




        public GameSpeedManager()
        {
            CurrentSpeed = GameSpeed.Slow;
            Time = DateTime.Now;
            minutesToday = Time.Minute + Time.Hour * 60;


        }

        public int CurrentSpeedToNum() //the actual meaning behind the enum
        {
            switch (CurrentSpeed)
            {
                // 1 IRL second = 10 game second
                case GameSpeed.Slow:
                    {
                        return 10;
                    }
                // 1 IRL second = 1 game minute
                case GameSpeed.Medium:
                    {
                        return 1 * 60;
                    }
                // 1 IRL second = 30 game minute
                case GameSpeed.Fast:
                    {
                        return 30 * 60;
                    }
            }
            throw new System.Exception("Invalid game speed");
        }
        public void AddTime(float delta)
        {
            double secondsInFrame = CurrentSpeedToNum() * delta;

            Time = Time.AddSeconds(secondsInFrame);
            minutesToday += secondsInFrame / 60;

            if (minutesToday > minutesInDay)
            {
                DayPassed?.Invoke(this, new EventArgs());
                minutesToday -= minutesInDay;
            }




        }


    }

    public enum GameSpeed { Slow, Medium, Fast } //might rename later



}
