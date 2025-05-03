using Safari.Model.Rangers;
using System;
using UnityEngine;

namespace Safari.Model.GameSpeed
{
    public class GameSpeedManager
    {
        public GameSpeed CurrentSpeed { get; set; }
        public DateTime Time { get; set; }
		public bool IsTimeRunning { get { return IsTimeRunning; } }

		private const int minutesInDay = 24 * 60;
        public double minutesToday;
     

        public event EventHandler DayPassed;

		private bool isTimeRunning = true;




		public GameSpeedManager() 
        {
            CurrentSpeed = GameSpeed.Slow;
            Time = DateTime.Now;
            minutesToday = Time.Minute + Time.Hour*60;
            
       
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
        public void AddTime(float delta)
        {
            double minutesInFrame = CurrentSpeedToNum() * delta * 10;

            Time = Time.AddMinutes(minutesInFrame);
            minutesToday += minutesInFrame;

            if(minutesToday > minutesInDay)
            {
                DayPassed?.Invoke(this, new EventArgs());
                minutesToday -= minutesInDay;
            }

       

            
        }

		public void StopTime()
		{
			isTimeRunning = false;
		}

		public void ResumeTime()
		{
			isTimeRunning = true;
		}


	}

    public enum GameSpeed { Slow, Medium, Fast } //might rename later



}
