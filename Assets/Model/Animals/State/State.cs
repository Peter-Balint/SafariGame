using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Animals.State
{
    public abstract class State
    {
        protected int thirst;

        protected Animal owner;


        public virtual void Update(float deltaTime)
        {
            thirst++;
        }

        protected State(Animal owner, int thirst)
        {
            this.owner = owner;
            this.thirst = thirst;
        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnExit()
        {
        }

        protected void TransitionTo(State newState)
        {
            owner.SetState(newState);
        }
    }
}
