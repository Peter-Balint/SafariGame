using Safari.Model.Pathfinding;
using UnityEngine;

namespace Safari.Model.Animals
{
    public abstract class Predator : Animal
    {
        public Predator(PathfindingHelper pathfinding) : base(pathfinding) { }

        public Predator(PathfindingHelper pathfinding, AnimalMetadata metadata) : base(pathfinding, metadata) { }

        public override State.State HandleFoodFinding()
        {
            throw new System.NotImplementedException();
        }
    }
}
