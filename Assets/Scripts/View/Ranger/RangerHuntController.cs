using Safari.Model.Animals;
using Safari.Model.Map;
using Safari.Model.Movement;
using Safari.Model.Rangers;
using Safari.View.Animals;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Safari.View.Rangers
{
    public class RangerHuntController : MonoBehaviour
    {
        public RangerCollectionController RangerCollectionController;
        public AnimalCollectionController AnimalCollectionController;
        
        private RangerDisplay currentRangerDisplay;

        List<RangerDisplay> removeRangerList;
        List<AnimalDisplay> removeAnimalList;

        CanvasRenderer canvasRenderer;

        private Dictionary<RangerDisplay, AnimalDisplay> huntMapping;

        void Start()
        {
            RangerCollectionController.OnRangerClicked += Open;
            huntMapping = new Dictionary<RangerDisplay, AnimalDisplay>();
            removeRangerList = new List<RangerDisplay>();
            removeAnimalList = new List<AnimalDisplay>();

            gameObject.transform.localScale = Vector3.zero;      
        }
        public void Open(object sender, RangerDisplay rangerDisplay)
        {
            gameObject.transform.localScale = Vector3.one;
            this.currentRangerDisplay = rangerDisplay;
        }
        public void Close()
        {
            gameObject.transform.localScale = Vector3.zero;
        }

        public void HuntWolf()
        {
            HuntPredator<Wolf>();
        }
        public void HuntLion()
        {
            HuntPredator<Lion>();
        }

        public void HuntPredator<PredatorType>()
        {
            double distanceSquare = 0;
            AnimalDisplay target = null;
            List<AnimalDisplay> animals = AnimalCollectionController.Displayers;

            Vector3 thisPosition = currentRangerDisplay.transform.position;
            Vector3 targetPosition;

            foreach (AnimalDisplay animalDisplay in animals)
            {
                if (animalDisplay.AnimalModel is PredatorType)
                {
                    if(target == null)
                    {
                        target = animalDisplay;
                        targetPosition = animalDisplay.transform.position;
                        distanceSquare = (targetPosition - thisPosition).sqrMagnitude;
                    }
                    else
                    {
                        targetPosition = animalDisplay.transform.position;
                        double newDistanceSquare = (targetPosition - thisPosition).sqrMagnitude;
                        if(newDistanceSquare < distanceSquare)
                        {
                            target = animalDisplay;
                            distanceSquare = newDistanceSquare;
                        }
                    }
                }
            }

            if (target != null)
            {
                currentRangerDisplay.Ranger.SetState(new Hunting(currentRangerDisplay.Ranger));

                if (!huntMapping.ContainsValue(target)) 
                {
                    target.AnimalModel.Movement.GridPositionChanged += OnTargetMoved;
                    target.AnimalModel.Died += OnTargetDied;
                }

                currentRangerDisplay?.Ranger.ModelUpdate(target.AnimalModel.Movement.Location);

                huntMapping[currentRangerDisplay] = target;
            }

            gameObject.transform.localScale = Vector3.zero;
        }

        private void OnTargetMoved(object sender, GridPosition gridPosition)
        {

            if (sender is MovementBehavior behavior)
            {
                foreach ((RangerDisplay rangerDisplay, AnimalDisplay animalDisplay) in huntMapping)
                {
                    if (animalDisplay.AnimalModel == behavior.Owner)
                    {
                        GridPosition targetGrid = animalDisplay.AnimalModel.Movement.Location;
                        rangerDisplay?.Ranger.ModelUpdate(targetGrid);  
                    }
                }
            }
        }
        private void OnTargetDied(object sender, EventArgs e)
        {
            List<RangerDisplay> list = new List<RangerDisplay>();
            if(sender is Animal animal)
            {
                foreach ((RangerDisplay rangerDisplay, AnimalDisplay animalDisplay) in huntMapping)
                {
                    if (animalDisplay.AnimalModel == animal)
                    {
                        list.Add(rangerDisplay);
                    }
                }
                foreach (RangerDisplay rangerDisplay in list)
                {
                    huntMapping.Remove(rangerDisplay);
                }
            }
        }

        void Update()
        {
            Vector3 rangerVector;
            Vector3 targetVector;
            removeRangerList.Clear();
            removeAnimalList.Clear();

            foreach ((RangerDisplay rangerDisplay, AnimalDisplay animalDisplay) in huntMapping)
            {
                rangerVector = rangerDisplay.transform.position;
                targetVector = animalDisplay.transform.position;
                if (rangerDisplay.Ranger.CheckInShootingDistance(rangerVector, targetVector))
                {
                    if (!removeAnimalList.Contains(animalDisplay))
                    {
                        removeAnimalList.Add(animalDisplay);
                    }
                }
            }
            foreach((RangerDisplay rangerDisplay, AnimalDisplay animalDisplay) in huntMapping)
            {
                if (removeAnimalList.Contains(animalDisplay))
                {
                    removeRangerList.Add(rangerDisplay);
                }
            }

            foreach (RangerDisplay rangerDisplay in removeRangerList)
            {
                rangerDisplay.Ranger.SetState(new Wandering(rangerDisplay.Ranger));
            }
            for (int i = 0; i < removeAnimalList.Count; i++)
            {
                removeAnimalList[i].AnimalModel.Kill();
            }
        }
    }
}
