#nullable enable
using Safari.Model.Movement;
using System;
using UnityEngine;

namespace Safari.Model.Animals.State
{
    public class Drinking : State
    {
        protected override bool DisableThirst => true;

        public Drinking(Animal owner, double hydrationPercent, float hunger) : base(owner, hydrationPercent, hunger)
        {
        }

        public override void Update(float deltaTime, int speedFactor)
        {
            double elapsedTimeAdjusted = (double)deltaTime * speedFactor;
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
            if (hydrationPercent < 0)
            {
                hydrationPercent = 0;
            }
            hydrationPercent += (elapsedTimeAdjusted / (owner.Metadata.TimeTillFullyHydrated * 60)) * 100;

            if (hydrationPercent >= 100)
            {
                hydrationPercent = 100;
                Debug.Log($"{owner.GetType().Name} is fully hydrated and will start wandering.");
                TransitionTo(new Wandering(owner, hydrationPercent, hunger));
            }
        }
    }
}
