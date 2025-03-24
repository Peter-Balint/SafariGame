using Safari.Model.Pathfinding;
using UnityEngine;

namespace Safari.Model.Animals
{
    public abstract class Herbivore : Animal
    {
        public Herbivore(PathfindingHelper pathfinding) : base(pathfinding) { }
    }
}
