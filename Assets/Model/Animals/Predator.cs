using Safari.Model.Animals.Movement;
using Safari.Model.Animals.State;
using Safari.Model.Movement;
using Safari.Model.Pathfinding;
using UnityEngine;

namespace Safari.Model.Animals
{
    public abstract class Predator : Animal
    {
        public Predator(PathfindingHelper pathfinding, Group group, Vector3 wordPos) : base(pathfinding, group, wordPos) { }

        public Predator(PathfindingHelper pathfinding, AnimalMetadata metadata, Group group, Vector3 wordPos) : base(pathfinding, metadata, group, wordPos) { }

        public override State.State HandleFoodFinding()
        {
            return new StalkingPrey(this, State.Thirst, State.Hunger);
        }
    }
}
