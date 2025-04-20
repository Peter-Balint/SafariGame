using NUnit.Framework;
using Safari.Model.Animals;
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
        
        private RangerDisplay rangerDisplay;

        void Start()
        {
            RangerCollectionController.OnRangerClicked += Open;
            gameObject.SetActive(false);
        }
        public void Open(object sender, RangerDisplay rangerDisplay)
        {
            gameObject.SetActive(true);
            this.rangerDisplay = rangerDisplay;
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

            foreach (AnimalDisplay animal in animals)
            {
                if (animal.AnimalModel is PredatorType)
                {
                    if(target == null)
                    {
                        target = animal;
                    }
                    else
                    {
                        Vector3 thisPosition = gameObject.transform.position;
                        Vector3 targetPosition = target.transform.position;
                        double newDistanceSquare = (targetPosition - thisPosition).sqrMagnitude;
                        if(newDistanceSquare > distance * distance)
                        {
                            target = animal;
                            distance = Math.Sqrt(newDistanceSquare);
                        }
                    }
                }
            }

            if (target != null)
            {

            }

        }

        void Update()
        {
        
        }
    }
}
