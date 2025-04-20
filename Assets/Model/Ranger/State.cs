using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Rangers
{
    public class State
    {
        protected Ranger owner;

        private bool transitioned = false;

        protected State(Ranger owner)
        {
            this.owner = owner;
        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnExit()
        {
        }

        protected void TransitionTo(State newState)
        {
            if (transitioned)
            {
                return;
            }
            owner.SetState(newState);
            transitioned = true;
        }
    }
}
