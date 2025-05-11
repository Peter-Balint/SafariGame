
using Safari.Model.GameSpeed;
using Safari.Model.Map;
using Safari.Model.Hunters;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using Safari.View.Animals;
using Safari.Model.Animals;
using System;

namespace Safari.View.Hunters
{
    public class HunterDisplay : MonoBehaviour
    {
        public Hunter? Hunter;

        private bool killed = false;

        public GameObject HunterPrefab;
        private GameObject? displayed;

        private GameSpeedManager gameSpeedManager;

        private Dictionary<GridPosition, Vector3> gridPositionMapping;

        private AnimalCollectionController animalCollectionController;

        private AnimalDisplay? targetDisplay;
        private string targetName;

        [SerializeField]
        private DeadAnimalPrefabMapping deadAnimalMapping;

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
            Hunter.EnteredLeaving += InstantiateCorpse;

            if (HunterPrefab == null) { return; }
            displayed = Instantiate(HunterPrefab, transform, false);

            this.gameSpeedManager = gameSpeedManager;
            var hunterMovement = displayed.GetComponent<HunterMovement>();
            var navMeshAgent = this.GetComponent<NavMeshAgent>();
            hunterMovement.Init(hunter.Movement, navMeshAgent, gridPositionMapping, gameSpeedManager);
        }

        private void SetToHunting(Hunter hunter)
        {
            int targetIndex = (int)Mathf.Floor(UnityEngine.Random.value * animalCollectionController.Displayers.Count);
            targetDisplay = animalCollectionController.Displayers[targetIndex];
            hunter.SetState(new Hunting(hunter));
            hunter.Target = targetDisplay.AnimalModel.Movement.Location;
            targetDisplay.AnimalModel.Movement.GridPositionChanged += (s, pos) => hunter.Target = pos;
            targetName = GetTargetName(targetDisplay.AnimalModel);
            targetDisplay.AnimalModel.Died += CheckPurposeAfterTargetDied;
        }

        private void CheckPurposeAfterTargetDied(object sender, EventArgs args)
        {
            if(!killed)
            {
                Hunter.Kill();
            }
        }

        private string GetTargetName(Animal animal) => animal switch
        {
            Wolf => "wolf",
            Camel => "camel",
            Sheep => "sheep",
            Lion => "lion",
            _ => throw new Exception()
        };

        private void InstantiateCorpse(object sender, EventArgs args)
        {
            GameObject deadAnimalPrefab = deadAnimalMapping.mappingDictionary[targetName];
            Instantiate(deadAnimalPrefab,transform,false);
        }


        private void Update()
        {
            if (!killed)
            {
                Vector3 hunterVector = this.transform.position;
                Vector3 targetVector = targetDisplay.transform.position;
                if (Hunter.CheckInShootingDistance(hunterVector, targetVector))
                {
                    killed = true;
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
