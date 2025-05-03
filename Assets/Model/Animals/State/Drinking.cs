#nullable enable
using Safari.Model.Movement;
using System;
using UnityEngine;

namespace Safari.Model.Animals.State
{
    public class Drinking : State
    {
        public Drinking(Animal owner, double hydrationPercent, float hunger) : base(owner, hydrationPercent, hunger)
        {
        }

        public override void Update(float deltaTime, int speedFactor)
        {
            base.Update(deltaTime, speedFactor);

            if (!owner.Pathfinding.IsDrinkingPlace(owner.Movement.Location))
            {
                Debug.Log($"{owner.GetType().Name} is no longer near a drinking place.");
                if (hydrationPercent < owner.Metadata.StillThirstyPercent)
                {
                    TransitionTo(new SearchingWater(owner, hydrationPercent, hunger));
                }
                else
                {
                    TransitionTo(new Wandering(owner, hydrationPercent, hunger));
                }
                return;
            }

            /*thirst += deltaTime * owner.DrinkingRate * speedFactor;*/

            if (hydrationPercent >= 100)
            {
                hydrationPercent = 100;
                Debug.Log($"{owner.GetType().Name} is fully hydrated and will start wandering.");
                TransitionTo(new Wandering(owner, hydrationPercent, hunger));
            }
        }
    }
}
