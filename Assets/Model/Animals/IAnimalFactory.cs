using Safari.Model.Pathfinding;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Safari.Model.Animals
{
    public interface IAnimalFactory
    {
        Animal CreateAnimal(Vector3 location, PathfindingHelper pathfinding);
    }
}
