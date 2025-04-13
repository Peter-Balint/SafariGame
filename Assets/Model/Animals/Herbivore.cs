using Safari.Model.Animals.State;
using Safari.Model.Pathfinding;
using UnityEngine;

namespace Safari.Model.Animals
{
    public abstract class Herbivore : Animal
    {
        public Herbivore(PathfindingHelper pathfinding) : base(pathfinding) { }

        public Herbivore(PathfindingHelper pathfinding, AnimalMetadata metadata) : base(pathfinding, metadata) { }

        public override State.State HandleFoodFinding()
        {
            return new SearchingFeedingSite(this, State.Thirst, State.Hunger);
        }
    }
}
