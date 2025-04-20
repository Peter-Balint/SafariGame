using Safari.Model.Map;
using Safari.Model.Pathfinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace Safari.Model.Animals
{
    public class AnimalCreationManager
    {
        private AnimalCollection animals;

        private PathfindingHelper pathfinding;

        private MoneyManager moneyManager;

        private Map.Map map;

        public AnimalCreationManager(AnimalCollection animals, PathfindingHelper pathfinding, Map.Map map, MoneyManager moneym)
        {
            this.animals = animals;
            this.pathfinding = pathfinding;
            this.map = map;
            this.moneyManager = moneym;
        }

        public void TryCreatingAnimal(GridPosition gridPosition, Vector3 position, IAnimalFactory animalFactory)
        {
            if (!IsGridPositionValid(gridPosition))
            {
                return;
            }

            
            //todo: add moneymanagement and pack behaviour code here, might need to change the signature of CreateAnimal for the latter
            Animal animal = animalFactory.CreateAnimal(position, pathfinding);
			if (moneyManager.CanBuy(animal.AnimalMetadata.Price))
			{
				Debug.Log(moneyManager.ReadBalance());
				animals.AddAnimal(animal);
				moneyManager.AddToBalance(-animal.AnimalMetadata.Price);
				Debug.Log(moneyManager.ReadBalance());
			}
			
        }

        private bool IsGridPositionValid(GridPosition gridPosition)
        {
            Field field = map.FieldAt(gridPosition);
            if (field is Ground || field is Road)
            {
                return true; ;
            }
            return false;
        }
    }
}
