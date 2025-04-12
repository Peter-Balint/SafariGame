#nullable enable
using Safari.Model.Movement;
using System;
using UnityEngine;

namespace Safari.Model.Animals.State
{
    public class Drinking : State
    {
        public Drinking(Animal owner, float thirst, float hunger) : base(owner, thirst, hunger)
        {
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (!owner.Pathfinding.IsDrinkingPlace(owner.Movement.Location))
            {
                Debug.Log($"{owner.GetType().Name} is no longer near a drinking place.");
                if (thirst > owner.ThirstLimit)
                {
                    TransitionTo(new SearchingWater(owner, thirst, hunger));
                }
                else
                {
                    TransitionTo(new Wandering(owner, thirst, hunger));
                }
                return;
            }

            thirst -= deltaTime * owner.DrinkingRate;

            if (thirst <= 0)
            {
                thirst = 0;
                Debug.Log($"{owner.GetType().Name} is fully hydrated and will start wandering.");
                TransitionTo(new Wandering(owner, thirst, hunger));
            }
        }
    }
}
