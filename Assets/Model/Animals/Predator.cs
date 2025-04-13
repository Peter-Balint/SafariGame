using Safari.Model.Animals.State;
using Safari.Model.Movement;
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
            return new Dead(this, State.Thirst, State.Hunger);
        }
    }
}
