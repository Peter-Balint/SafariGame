using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.Model.Animals.State
{
    public class PredatorEating : State
    {
        protected override bool DisableHunger => true;

        public PredatorEating(Animal owner, double hydrationPercent, double saturationPercent) : base(owner, hydrationPercent, saturationPercent)
        {
        }

        public override void Update(float deltaTime, int speedFactor)
        {
            base.Update(deltaTime, speedFactor);
            double elapsedTimeAdjusted = (double)deltaTime * speedFactor;

            if (saturationPercent < 0)
            {
                saturationPercent = 0;
            }
            saturationPercent += (elapsedTimeAdjusted / (owner.Metadata.TimeTillFullySaturated * 60)) * 100;

            if (saturationPercent >= 100)
            {
                saturationPercent = 100;
                Debug.Log($"{owner.GetType().Name} is no longer hungry and will rest now.");
                TransitionTo(new Resting(owner, hydrationPercent, saturationPercent));
            }
        }
    }
}
