using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.Model.Animals.State
{
    public class Resting : State
    {
        private float restingSince = 0;

        private float restingDuration = -1;

        public Resting(Animal owner, double hydrationPercent, double saturationPercent) : base(owner, hydrationPercent, saturationPercent)
        {
        }

        public Resting(Animal owner, double hydrationPercent, double saturationPercent, float duration) : base(owner, hydrationPercent, saturationPercent)
        {
            restingDuration = duration;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            if (owner.Group?.Animals?.Count > 1)
            {
                Debug.Log("Reuniting");
                TransitionTo(new Reuniting(owner, hydrationPercent, saturationPercent, (t, h) => new Resting(owner, t, h)));
                return;
            }

            owner.Movement.AbortMovement();
            if (restingDuration < 0)
            {
                restingDuration = UnityEngine.Random.Range(owner.Metadata.RestingTimeMin, owner.Metadata.RestingTimeMax);
            }
            Debug.Log($"Resting for {restingDuration} minutes");
        }

        public override void Update(float deltaTime, int speedFactor)
        {
            base.Update(deltaTime, speedFactor);
            restingSince += deltaTime * speedFactor / 60;
            AllowSearchingWater();
            AllowSearchingFood();
            if (restingSince > restingDuration)
            {
                TransitionTo(new Wandering(owner, hydrationPercent, saturationPercent));
            }
        }
    }
}
