﻿using Safari.Model.Animals.Movement;
using Safari.Model.Animals.State;
using Safari.Model.Movement;
using Safari.Model.Pathfinding;
using UnityEngine;

namespace Safari.Model.Animals
{
    public abstract class Predator : Animal
    {
        public Predator(PathfindingHelper pathfinding, Group group, AnimalCollection collection, Vector3 wordPos) : base(pathfinding, group, collection, wordPos) { }

        public Predator(PathfindingHelper pathfinding, AnimalMetadata metadata, Group group, AnimalCollection collection, Vector3 wordPos) : base(pathfinding, metadata, group, collection, wordPos) { }

        public override State.State HandleFoodFinding()
        {
            return new StalkingPrey(this, State.HydrationPercent, State.SaturationPercent, State.BreedingCooldown);
        }
    }
}
