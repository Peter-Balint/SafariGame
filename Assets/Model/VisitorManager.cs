using Safari.Model.GameSpeed;
using Safari.Model.Rangers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.Model.Assets
{
    public class VisitorManager
    {
        public int VisitorsWaiting { get; private set; }

        public int ActualVisitors {  get; private set; }

        private int visitorSpawnedSec = 0;

        private readonly MoneyManager moneyManager;

        private readonly GameSpeedManager speedManager;

        public VisitorManager(MoneyManager moneyManager, GameSpeedManager speedManager)
        {
            this.moneyManager = moneyManager;
            this.speedManager = speedManager;
        }
        /// <summary>
        /// in every 10th game-minute there is a chance of a visitor spawning 
        /// </summary>
        public void Update()
        {
            if ((int)speedManager.minutesToday % 10 == 0 && visitorSpawnedSec != (int)speedManager.minutesToday)
            {
                moneyManager.CalculateVisitDesire();
                VisitorSpawn();
                visitorSpawnedSec = (int)speedManager.minutesToday;
            }
        }

        /// <summary>
        /// Subtracts the visitors from the waiting line, and counts them for checking win conditions.
        /// </summary>
        /// <param name="capacity"></param>
        /// <returns></returns>

        internal int TakeVisitors(int capacity)
        {
            if (VisitorsWaiting >= capacity)
            {
                VisitorsWaiting -= capacity;
                ActualVisitors += capacity;
                return capacity;
            }

            int temp = VisitorsWaiting;
            VisitorsWaiting = 0;
            return temp;
        }
        /// <summary>
        /// Spawns a visitor if the random roll is lower than the visit desire chance.
        /// Spawned visitors already paid for their tickets.
        /// They have to wait until there is free jeep-capacity.
        /// </summary>
        private void VisitorSpawn()
        {
            double random = UnityEngine.Random.Range(0, 9);
            random *= 0.1;

            if (moneyManager.ReadVisitDesire() > random)
            {
                VisitorsWaiting++;
                moneyManager.AddToBalance(moneyManager.ReadTicketPrice());
            }
        }

    }
}
