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

        private float restingDuration;

        public Resting(Animal owner, float thirst) : base(owner, thirst)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            owner.Movement.AbortMovement();

            restingDuration = UnityEngine.Random.Range(owner.RestingInterval.Item1, owner.RestingInterval.Item2);
            Debug.Log($"Resting for {restingDuration} seconds");
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            restingSince += deltaTime;
            if (restingSince > restingDuration)
            {
                TransitionTo(new Wandering(owner, thirst));
            }
            AllowSearchingWater();
        }
    }
}
