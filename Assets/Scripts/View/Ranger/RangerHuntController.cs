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


        private Dictionary<RangerDisplay, AnimalDisplay> huntMapping;

        void Start()
        {
            RangerCollectionController.OnRangerClicked += Open;
            huntMapping = new Dictionary<RangerDisplay, AnimalDisplay>();
            gameObject.SetActive(false);
        }
        public void Open(object sender, RangerDisplay rangerDisplay)
        {
            gameObject.SetActive(true);
            this.currentRangerDisplay = rangerDisplay;
        }
        public void Close()
        {
            gameObject.SetActive(false);
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
            double distance = 0;
            AnimalDisplay target = null;
            List<AnimalDisplay> animals = AnimalCollectionController.Displayers;

            foreach (AnimalDisplay animalDisplay in animals)
            {
                if (animalDisplay.AnimalModel is PredatorType)
                {
                    if(target == null)
                    {
                        target = animalDisplay;
                    }
                    else
                    {
                        Vector3 thisPosition = gameObject.transform.position;
                        Vector3 targetPosition = target.transform.position;
                        double newDistanceSquare = (targetPosition - thisPosition).sqrMagnitude;
                        if(newDistanceSquare > distance * distance)
                        {
                            target = animalDisplay;
                            distance = Math.Sqrt(newDistanceSquare);
                        }
                    }
                }
            }

            if (target != null)
            {
                currentRangerDisplay.Ranger.SetState(new Hunting(currentRangerDisplay.Ranger));
                if(!target.AnimalModel.Movement.HasSubscribers) { target.AnimalModel.Movement.GridPositionChanged += OnTargetMoved; }
                currentRangerDisplay?.Ranger.ModelUpdate(target.AnimalModel.Movement.Location);

                huntMapping[currentRangerDisplay] = target;
            }

            gameObject.SetActive(false);
        }

        private void OnTargetMoved(object sender, GridPosition gridPosition)
        {
            if(sender is MovementBehavior behavior)
            {
                foreach((RangerDisplay rangerDisplay,AnimalDisplay animalDisplay) in huntMapping)
                {
                    if(animalDisplay.AnimalModel == behavior.Owner)
                    {
                        rangerDisplay?.Ranger.ModelUpdate(animalDisplay.AnimalModel.Movement.Location);
                    }
                }
            }// for some reason two rangers going after the same animal freezes both of them
        }

        void Update()
        {
        }
    }
}
