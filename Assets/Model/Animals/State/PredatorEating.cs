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
        public PredatorEating(Animal owner, float thirst, float hunger) : base(owner, thirst, hunger)
        {
        }

        public override void Update(float deltaTime, int speedFactor)
        {
            base.Update(deltaTime, speedFactor);

            hunger -= deltaTime * owner.EatingRate * speedFactor;

            if (hunger <= 0)
            {
                hunger = 0;
                Debug.Log($"{owner.GetType().Name} is no longer hungry and will rest now.");
                TransitionTo(new Resting(owner, thirst, hunger));
            }
        }
    }
}
