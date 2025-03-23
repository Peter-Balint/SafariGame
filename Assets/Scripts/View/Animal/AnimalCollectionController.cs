using Safari.Model;
using Safari.Model.Animals;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace Safari.View
{
    public class AnimalCollectionController
    {
        private List<AnimalDisplay> displayers;
        private AnimalCollection animalCollection;

        public AnimalCollectionController(AnimalCollection animalCollection)
        {
            this.animalCollection = animalCollection;
            displayers = new List<AnimalDisplay>();

            animalCollection.Added += OnAnimalAdded;
            animalCollection.Removed += OnAnimalRemoved;
        }

        private void OnAnimalAdded(object sender, Animal animal)
        {
            AnimalDisplay display = new AnimalDisplay();
            display.Init(animal, animal.Position);
            displayers.Add(display);
        }

        private void OnAnimalRemoved(object sender, Animal animal)
        {
            foreach (AnimalDisplay display in displayers) 
            { 
                if(display.AnimalModel == animal)
                {
                    displayers.Remove(display);
                    return;
                }
            }
        }
    }
}
