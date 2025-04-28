using Safari.Model.Animals.State;
using Safari.Model.Pathfinding;
using UnityEngine;

namespace Safari.Model.Animals
{
    public abstract class Herbivore : Animal
    {
        public Herbivore(PathfindingHelper pathfinding, Group group, Vector3 wordPos) : base(pathfinding, group, wordPos) { }

        public Herbivore(PathfindingHelper pathfinding, AnimalMetadata metadata, Group group, Vector3 wordPos) : base(pathfinding, metadata, group, wordPos) { }

        public override State.State HandleFoodFinding()
        {
            return new SearchingFeedingSite(this, State.Thirst, State.Hunger);
        }
    }
}
