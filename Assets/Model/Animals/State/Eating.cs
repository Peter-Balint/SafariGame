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
        public Eating(Animal owner, float thirst, float hunger) : base(owner, thirst, hunger)
        {
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (!owner.Pathfinding.IsFeedingSite(owner.Movement.Location))
            {
                Debug.Log($"{owner.GetType().Name} is no longer near a feeding site.");
                if (hunger > owner.HungerLimit)
                {
                    TransitionTo(new SearchingFeedingSite(owner, thirst, hunger));
                }
                else
                {
                    TransitionTo(new Wandering(owner, thirst, hunger));
                }
                return;
            }

            hunger -= deltaTime * owner.EatingRate;

            if (hunger <= 0)
            {
                hunger = 0;
                Debug.Log($"{owner.GetType().Name} is no longer hungry and will rest now.");
                TransitionTo(new Resting(owner, thirst, hunger));
            }
        }
    }
}
