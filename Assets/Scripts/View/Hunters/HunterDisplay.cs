
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
using Safari.View.Rangers;
using static UnityEditor.Experimental.GraphView.GraphView;
using Safari.View.Jeeps;

namespace Safari.View.Hunters
{
    public class HunterDisplay : MonoBehaviour
    {
        public Hunter? Hunter;

        [SerializeField]
        private int revealDistance;
        private float closestRevealerSqr = float.MaxValue;
        private bool visible = false;

        private bool killed = false;

        public GameObject HunterPrefab;
        private GameObject? displayed;
        private SkinnedMeshRenderer hunterMesh;
        private HunterMovement hunterMovement;

        private GameSpeedManager gameSpeedManager;

        private Dictionary<GridPosition, Vector3> gridPositionMapping;

        private AnimalCollectionController animalCollectionController;
        private JeepCollectionController jeepCollectionController;
        private RangerCollectionController rangerCollectionController;

        private AnimalDisplay? targetDisplay;
        private string targetName;

        private GameObject deadAnimalPrefab;
        private SkinnedMeshRenderer deadAnimalMesh;

        [SerializeField]
        private DeadAnimalPrefabMapping deadAnimalMapping;

        public void Init(Hunter hunter, Dictionary<GridPosition, Vector3> gridPosMapping, GameSpeedManager gameSpeedManager,
                        AnimalCollectionController animalCollectionController, JeepCollectionController jeepCollectionController, RangerCollectionController rangerCollectionController)
        {
            gridPositionMapping = gridPosMapping;
            this.animalCollectionController = animalCollectionController;
            this.jeepCollectionController = jeepCollectionController;
            this.rangerCollectionController = rangerCollectionController;

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
            hunterMovement = displayed.GetComponent<HunterMovement>();

            hunterMovement.MiniMapIcon.SetActive(false);

            var navMeshAgent = this.GetComponent<NavMeshAgent>();

            hunterMesh = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            hunterMesh.enabled = false;

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
            deadAnimalPrefab = deadAnimalMapping.mappingDictionary[targetName];
            deadAnimalPrefab = Instantiate(deadAnimalPrefab,transform,false);
            if (!visible)
                    {
                        deadAnimalMesh = deadAnimalPrefab.GetComponentInChildren<SkinnedMeshRenderer>();
                        deadAnimalMesh.enabled = false;
                    }
        }

        private void SetToVisible()
        {
            hunterMesh.enabled = true;
            if(deadAnimalMesh != null)
            {
                deadAnimalMesh.enabled = true;
            }
            hunterMovement.MiniMapIcon.SetActive(true);
        }
        private void SetToInvisible()
        {
            hunterMesh.enabled = false;
            if (deadAnimalMesh != null)
            {
                deadAnimalMesh.enabled = false;
            }
            hunterMovement.MiniMapIcon.SetActive(false);
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

            float distanceSqr = 0;
            closestRevealerSqr = float.MaxValue;
            foreach (RangerDisplay rangerDisplay in rangerCollectionController.displayers)
            {
                distanceSqr = (rangerDisplay.transform.position - transform.position).sqrMagnitude;
                if (distanceSqr < closestRevealerSqr)
                {
                    closestRevealerSqr = distanceSqr;
                    if(Hunter.CheckInShootingDistance(transform.position, rangerDisplay.transform.position))
                    {
                        float rnd = UnityEngine.Random.value;
                        if(rnd < 0.5)
                        {
                            Hunter.Kill();
                        }
                        else
                        {
                            rangerDisplay.Ranger.Kill();
                        }
                    }
                }
            }
            foreach (JeepDisplay jeepDisplay in jeepCollectionController.Displayers)
            {
                distanceSqr = (jeepDisplay.transform.position - transform.position).sqrMagnitude;
                if (distanceSqr < closestRevealerSqr)
                {
                    closestRevealerSqr = distanceSqr;
                }
            }
            if (closestRevealerSqr <= revealDistance * revealDistance && !visible)
            {
                SetToVisible();
                visible = true;
            }
            else if (closestRevealerSqr > revealDistance * revealDistance && visible)
            {
                SetToInvisible();
                visible = false;
            }
        }
    }
}
