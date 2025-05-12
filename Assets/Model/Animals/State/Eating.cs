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
        protected override bool DisableHunger => true;

        public Eating(Animal owner, double hydrationPercent, double saturationPercent, double breedingCooldown) : base(owner, hydrationPercent, saturationPercent, breedingCooldown)
        {
        }

        public override void Update(float deltaTime, int speedFactor)
        {
            base.Update(deltaTime, speedFactor);
            double elapsedTimeAdjusted = (double)deltaTime * speedFactor;

            if (!owner.Pathfinding.IsFeedingSite(owner.Movement.Location))
            {
                Debug.Log($"{owner.GetType().Name} is no longer near a feeding site.");
                if (saturationPercent < owner.Metadata.StillHungryPercent)
                {
                    TransitionTo(new SearchingFeedingSite(owner, hydrationPercent, saturationPercent, breedingCooldown));
                }
                else
                {
                    TransitionTo(new Wandering(owner, hydrationPercent, saturationPercent, breedingCooldown));
                }
                return;
            }

            if (saturationPercent < 0)
            {
                saturationPercent = 0;
            }
            saturationPercent += (elapsedTimeAdjusted / (owner.Metadata.TimeTillFullySaturated * 60)) * 100;

            if (saturationPercent >= 100)
            {
                saturationPercent = 100;
                Debug.Log($"{owner.GetType().Name} is no longer hungry and will rest now.");
                TransitionTo(new Resting(owner, hydrationPercent, saturationPercent, breedingCooldown));
            }
        }
    }
}
