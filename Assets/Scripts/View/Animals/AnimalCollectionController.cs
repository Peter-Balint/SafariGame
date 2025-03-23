using Safari.Model;
using Safari.Model.Animals;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace Safari.View.Animals
{
    public class AnimalCollectionController : MonoBehaviour
    {
        private List<AnimalDisplay> displayers;
        private AnimalCollection animalCollection;

        public AnimalDisplay AnimalDisplayPrefab;

        public void Start()
        {
            animalCollection = SafariGame.Instance.Animals;
            displayers = new List<AnimalDisplay>();

            animalCollection.Added += OnAnimalAdded;
            animalCollection.Removed += OnAnimalRemoved;

            foreach (Animal animal in animalCollection.Animals) 
            {
                OnAnimalAdded(null, animal);
            }
        }

        private void OnAnimalAdded(object sender, Animal animal)
        {
            AnimalDisplay display = Instantiate(AnimalDisplayPrefab, animal.Position,
                Quaternion.identity,
                new InstantiateParameters()
                {
                    parent = transform,
                    worldSpace = false
                });
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
                    Destroy(display);
                    return;
                }
            }
        }
    }
}
