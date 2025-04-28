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

        public Resting(Animal owner, float thirst, float hunger) : base(owner, thirst, hunger)
        {
        }

        public Resting(Animal owner, float thirst, float hunger, float duration) : base(owner, thirst, hunger)
        {
            restingDuration = duration;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            owner.Movement.AbortMovement();
            if (restingDuration < 0)
            {
                restingDuration = UnityEngine.Random.Range(owner.RestingInterval.Item1, owner.RestingInterval.Item2);
            }
            Debug.Log($"Resting for {restingDuration} seconds");
        }

        public override void Update(float deltaTime, int speedFactor)
        {
            base.Update(deltaTime, speedFactor);
            restingSince += deltaTime * speedFactor;
            AllowSearchingWater();
            AllowSearchingFood();
            if (restingSince > restingDuration)
            {
                TransitionTo(new Wandering(owner, thirst, hunger));
            }
        }
    }
}
