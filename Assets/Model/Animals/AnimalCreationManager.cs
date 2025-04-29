using Safari.Model.Map;
using Safari.Model.Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;


namespace Safari.Model.Animals
{
    public class AnimalCreationManager
    {
        private const float GroupTooFarLimit = 200f;

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
            
            Animal animal = animalFactory.CreateAnimal(position, pathfinding);
			if (moneyManager.CanBuy(animal.AnimalMetadata.Price))
			{
				Debug.Log(moneyManager.ReadBalance());
                animal.Group = FindOrCreateGroup(animal);
                animal.Group.AddAnimal(animal);
                animals.AddAnimal(animal);
				moneyManager.AddToBalance(-animal.AnimalMetadata.Price);
				Debug.Log(moneyManager.ReadBalance());
			}
			
        }

        private Group FindOrCreateGroup(Animal animal)
        {
            Animal closest = null;
            float closestDistance = float.MaxValue;
            foreach (var otherAnimal in animals.Animals)
            {
                if (otherAnimal == animal || otherAnimal.GetType() != animal.GetType())
                {
                    continue;
                }
                float distance = Vector3.Distance(animal.Movement.WordPos, otherAnimal.Movement.WordPos);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = otherAnimal;
                }
            }
            if (closest == null || closestDistance > GroupTooFarLimit)
            {
                return new Group();
            }
            return closest.Group;
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
