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
        private int visitorSpawnedSec = 0;

        private readonly MoneyManager moneyManager;

        private readonly GameSpeedManager speedManager;

        public VisitorManager(MoneyManager moneyManager, GameSpeedManager speedManager)
        {
            this.moneyManager = moneyManager;
            this.speedManager = speedManager;
        }

        public void Update()
        {
            if ((int)speedManager.minutesToday % 10 == 0 && visitorSpawnedSec != (int)speedManager.minutesToday)
            {
                moneyManager.CalculateVisitDesire();
                VisitorSpawn();
                visitorSpawnedSec = (int)speedManager.minutesToday;
            }
        }

        private void VisitorSpawn()
        {
            double random = UnityEngine.Random.Range(0, 9);
            random *= 0.1;

            if (moneyManager.ReadVisitDesire() > random)
            {
                moneyManager.AddToBalance(moneyManager.ReadTicketPrice());
            }
        }
    }
}
