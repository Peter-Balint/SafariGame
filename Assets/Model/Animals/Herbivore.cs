using Safari.Model.Animals.Movement;
using Safari.Model.Animals.State;
using Safari.Model.Pathfinding;
using UnityEngine;

namespace Safari.Model.Animals
{
    public abstract class Herbivore : Animal, IPrey
    {
        public Herbivore(PathfindingHelper pathfinding) : base(pathfinding) { }

        public Herbivore(PathfindingHelper pathfinding, AnimalMetadata metadata) : base(pathfinding, metadata) { }

        public override State.State HandleFoodFinding()
        {
            return new SearchingFeedingSite(this, State.Thirst, State.Hunger);
        }

        public void Kill()
        {
            InterruptState(new Dead(this, State.Thirst, State.Hunger));
        }

        public void OnChased(Chaser chaser)
        {
            // interrupt the current state and start fleeing
            InterruptState(new Fleeing(this, State.Thirst, State.Hunger, chaser));
        }
    }
}
