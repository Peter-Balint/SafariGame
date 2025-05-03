using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.Model.Animals.State
{
    public class Eating : State
    {
        public Eating(Animal owner, double hydrationPercent, float hunger) : base(owner, hydrationPercent, hunger)
        {
        }

        public override void Update(float deltaTime, int speedFactor)
        {
            base.Update(deltaTime, speedFactor);

            if (!owner.Pathfinding.IsFeedingSite(owner.Movement.Location))
            {
                Debug.Log($"{owner.GetType().Name} is no longer near a feeding site.");
               /* if (hunger > owner.HungerLimit)
                {
                    TransitionTo(new SearchingFeedingSite(owner, hydrationPercent, hunger));
                }
                else
                {
                    TransitionTo(new Wandering(owner, hydrationPercent, hunger));
                }*/
                return;
            }

            /*hunger -= deltaTime * owner.EatingRate * speedFactor;*/

            if (hunger <= 0)
            {
                hunger = 0;
                Debug.Log($"{owner.GetType().Name} is no longer hungry and will rest now.");
                TransitionTo(new Resting(owner, hydrationPercent, hunger));
            }
        }
    }
}
