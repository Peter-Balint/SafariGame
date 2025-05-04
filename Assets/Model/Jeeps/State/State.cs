using Safari.Model.Animals.State;
using Safari.Model.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Safari.Model.Assets;

namespace Safari.Model.Jeeps.State
{
    public abstract class State
    {
        protected Jeep owner;

        protected VisitorManager visitorManager;

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

        protected State(Jeep owner, VisitorManager visitorManager)
        {
            this.owner = owner;
            this.visitorManager = visitorManager;
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
