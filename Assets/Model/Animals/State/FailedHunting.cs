using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.Model.Animals.State
{
    public class FailedHunting : State
    {
        private float restingSince = 0;

        private float restingDuration;

        public FailedHunting(Animal owner, double hydrationPercent, double saturationPercent) : base(owner, hydrationPercent, saturationPercent)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            owner.Movement.AbortMovement();

            restingDuration = UnityEngine.Random.Range(owner.Metadata.RestingTimeMin, owner.Metadata.RestingTimeMax);
            Debug.Log($"{owner.GetType().Name} failed to catch the prey. Resting for {restingDuration} minutes");
        }

        public override void Update(float deltaTime, int speedFactor)
        {
            base.Update(deltaTime, speedFactor);
            restingSince += deltaTime * speedFactor / 60;
            if (restingSince > restingDuration)
            {
                TransitionTo(new Wandering(owner, hydrationPercent, saturationPercent));
            }
        }
    }
}
