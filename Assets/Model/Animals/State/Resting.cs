using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Animals.State
{
    public class Resting : Animals.State.State
    {
        private float restingSince = 0;

        private float restingDuration;

        public Resting(Animal owner, int thirst) : base(owner, thirst)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            owner.Movement.AbortMovement();

            Random r = new Random();
            restingDuration = (float)r.NextDouble()
                              * (owner.RestingInterval.Item2 - owner.RestingInterval.Item1)
                              + owner.RestingInterval.Item1;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            restingSince += deltaTime;
            if (restingSince > restingDuration)
            {
                TransitionTo(new Wandering(owner, thirst));
            }
            if (thirst > owner.ThirstLimit)
            {
                
            }
        }
    }
}
