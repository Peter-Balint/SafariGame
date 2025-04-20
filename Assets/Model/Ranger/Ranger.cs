using Safari.Model.Movement;
using System;
using UnityEngine;

namespace Safari.Model.Rangers
{
    public class Ranger : IMoving
    {
        public Vector3 Position {  get; set; }

        public MovementBehavior Movement;

        public event EventHandler? Died;

        private State state;

        public Ranger() 
        {
            Movement = new MovementBehavior(this);
            Position = Vector3.zero;
            state = new Wandering(this);
            state.OnEnter();
        }

        public void SetState(State state)
        {
            this.state.OnExit();
            this.state = state;
            this.state.OnEnter();
            if (this.state is Dead)
            {
                Died?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
