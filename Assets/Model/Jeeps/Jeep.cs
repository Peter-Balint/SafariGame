using Safari.Model.Map;
using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

namespace Safari.Model.Jeeps
{
    public class Jeep : IMoving
    {
        public event EventHandler? StateChanged;

        public Vector3 Position { get; set; }
        public MovementBehavior Movement;

		public State.State State { get; private set; }

        public Jeep(Vector3 vec3)
		{
			Position = vec3;
			Movement = new MovementBehavior(this, Position);
		}

        internal void SetState(State.State state)
        {
            State.OnExit();
            State = state;
            State.OnEnter();
            StateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
