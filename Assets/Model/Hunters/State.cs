using Safari.Model.Map;
using System;
using UnityEngine;

namespace Safari.Model.Hunters
{
    public class State
    {
        protected Hunter owner;

        protected Map.Map map;

        private bool transitioned = false;

        protected State(Hunter owner)
        {
            this.owner = owner;
            map = SafariGame.Instance.Map;
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

        public virtual void Update()
        {
        }

        public virtual void OnTargetChanged()
        {

        }
    }
}
