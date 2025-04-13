using Safari.Model.Map;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace Safari.Model.Animals
{
    public class AnimalCreationManager
    {
        private readonly Dictionary<GridPosition, Vector3> gridPositionMapping;
        private AnimalCollection animals;
        public AnimalCreationManager(Dictionary<GridPosition, Vector3> gridPositionMapping)
        {
            this.gridPositionMapping = gridPositionMapping;
            animals = SafariGame.Instance.Animals;
        }


        public void TryCreatingAnimal(GridPosition gridPosition, IAnimalFactory animalFactory)
        {
            if (!IsGridPositionValid(gridPosition)) 
            { 
                return;
            }

            //todo: add moneymanagement and pack behaviour code here, might need to change the signature of CreateAnimal for the latter

            Animal animal = animalFactory.CreateAnimal(gridPositionMapping[gridPosition], SafariGame.Instance.pathfinding);
            animals.AddAnimal(animal);
        }

        private bool IsGridPositionValid(GridPosition gridPosition) 
        {
            Field field = SafariGame.Instance.Map.FieldAt(gridPosition);
            if (field is Ground || field is Road)
            {
                return true; ;
            }
            return false;
        }
    }
}
