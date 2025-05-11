
using Safari.Model.GameSpeed;
using Safari.Model.Map;
using Safari.Model.Hunters;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using Safari.View.Animals;

namespace Safari.View.Hunters
{
    public class HunterDisplay : MonoBehaviour
    {
        public Hunter? Hunter;

        public GameObject HunterPrefab;
        private GameObject? displayed;

        private GameSpeedManager gameSpeedManager;

        private Dictionary<GridPosition, Vector3> gridPositionMapping;

        private AnimalCollectionController animalCollectionController;

        private AnimalDisplay? targetDisplay;

        public void Init(Hunter hunter, Dictionary<GridPosition, Vector3> gridPosMapping, GameSpeedManager gameSpeedManager, AnimalCollectionController animalCollectionController)
        {
            gridPositionMapping = gridPosMapping;
            this.animalCollectionController = animalCollectionController;
            Trace.Assert(displayed == null);
            DisplayHunter(hunter, gameSpeedManager);
            SetToHunting(hunter);
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

        private void SetToHunting(Hunter hunter)
        {
            int targetIndex = (int)Mathf.Floor(Random.value * animalCollectionController.Displayers.Count);
            targetDisplay = animalCollectionController.Displayers[targetIndex];
            hunter.SetState(new Hunting(hunter));
            hunter.Target = targetDisplay.AnimalModel.Movement.Location;
            targetDisplay.AnimalModel.Movement.GridPositionChanged += (s, pos) => hunter.Target = pos;
        }

        private void Update()
        {
            if (targetDisplay != null)
            {
                Vector3 hunterVector = this.transform.position;
                Vector3 targetVector = targetDisplay.transform.position;
                if (Hunter.CheckInShootingDistance(hunterVector, targetVector))
                {
                    targetDisplay.AnimalModel.Kill();
                    targetDisplay = null;
                }
            }
            else
            {
                Hunter.ModelUpdate();
            }
        }
    }
}
