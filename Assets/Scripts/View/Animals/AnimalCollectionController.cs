using Safari.Model;
using Safari.Model.Animals;
using UnityEngine;
using System.Collections.Generic;
using System;
using Safari.View.World.Map;
using Safari.Model.Map;

namespace Safari.View.Animals
{
    public class AnimalCollectionController : MonoBehaviour
    {
        private List<AnimalDisplay> displayers;
        private AnimalCollection animalCollection;

        public AnimalDisplay AnimalDisplayPrefab;

        private Dictionary<GridPosition, Vector3> gridPositionMapping;

        public void Start()
        {
            animalCollection = SafariGame.Instance.Animals;
            displayers = new List<AnimalDisplay>();

        }

        public void InjectGridPositionMappingData(MapDisplay.MapInitializedEventArgs args)
        {
            gridPositionMapping = new Dictionary<GridPosition, Vector3>();
            foreach (var item in args.Displayers)
            {
                gridPositionMapping.Add(item.Value.Position, item.Key);
            }
        }

        public void Init()
        {
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
            display.Init(animal, animal.Position, gridPositionMapping);
            displayers.Add(display);
        }

        private void OnAnimalRemoved(object sender, Animal animal)
        {
            foreach (AnimalDisplay display in displayers) 
            { 
                if(display.AnimalModel == animal)
                {
                    displayers.Remove(display);
                    Destroy(display.gameObject);
                    return;
                }
            }
        }
    }
}
