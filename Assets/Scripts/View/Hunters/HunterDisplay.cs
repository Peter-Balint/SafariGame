using Safari.Model.GameSpeed;
using Safari.Model.Map;
using Safari.Model.Hunters;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

namespace Safari.View.Hunters
{
    public class HunterDisplay : MonoBehaviour
    {
        public Hunter? Hunter;

        public GameObject HunterPrefab;
        private GameObject? displayed;

        private GameSpeedManager gameSpeedManager;

        private Dictionary<GridPosition, Vector3> gridPositionMapping;

        public void Init(Hunter hunter, Dictionary<GridPosition, Vector3> gridPosMapping, GameSpeedManager gameSpeedManager)
        {
            gridPositionMapping = gridPosMapping;
            Trace.Assert(displayed == null);
            DisplayHunter(hunter, gameSpeedManager);
        }

        private void DisplayHunter(Hunter hunter, GameSpeedManager gameSpeedManager)
        {
            this.Hunter = hunter;

            if (HunterPrefab == null) { return; }
            displayed = Instantiate(HunterPrefab, transform, false);

            this.gameSpeedManager = gameSpeedManager;
            var hunterMovement = displayed.GetComponent<HunterMovement>();
            var navMeshAgent = this.GetComponent<NavMeshAgent>();
            hunterMovement.Init(hunter.Movement, navMeshAgent, gridPositionMapping, gameSpeedManager);
        }
    }
}
