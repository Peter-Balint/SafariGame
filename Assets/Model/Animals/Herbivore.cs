using Safari.Model.Animals.Movement;
using Safari.Model.Animals.State;
using Safari.Model.Pathfinding;
using UnityEngine;

namespace Safari.Model.Animals
{
    public abstract class Herbivore : Animal, IPrey
    {
        public Herbivore(PathfindingHelper pathfinding, Group group, AnimalCollection collection, Vector3 wordPos) : base(pathfinding, group, collection, wordPos) { }

        public Herbivore(PathfindingHelper pathfinding, AnimalMetadata metadata, Group group, AnimalCollection collection, Vector3 wordPos) : base(pathfinding, metadata, group, collection, wordPos) { }

        public override State.State HandleFoodFinding()
        {
            return new SearchingFeedingSite(this, State.HydrationPercent, State.SaturationPercent, State.BreedingCooldown);
        }

        public void OnChased(Chaser chaser)
        {
            // interrupt the current state and start fleeing
            InterruptState(new Fleeing(this, State.HydrationPercent, State.SaturationPercent, State.BreedingCooldown, chaser));
        }

        public void OnEscaped()
        {
            Debug.Log($"{GetType().Name} escaped from the predator");
            InterruptState(new Resting(this, State.HydrationPercent, State.SaturationPercent, State.BreedingCooldown));
        }
    }
}
