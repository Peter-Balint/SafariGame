using Safari.Model.Animals.State;
using Safari.Model.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Jeeps.State
{
    public abstract class State
    {
        protected Jeep owner;

        private bool transitioned = false;

        private Queue<Action> actionQueue = new Queue<Action>();

        public virtual void Update(float deltaTime, int speedFactor)
        {
            while (actionQueue.Count > 0)
            {
                var action = actionQueue.Dequeue();
                action();
            }
        }

        protected State(Jeep owner)
        {
            this.owner = owner;

        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnExit()
        {
        }

        public virtual void OnInterrupted()
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

        protected void TransitionToNextUpdate(State state)
        {
            NextUpdate(() => TransitionTo(state));
        }

        protected virtual void NextUpdate(Action action)
        {
            actionQueue.Enqueue(action);
        }
    }
}
