using Safari.Model.Assets;
using Safari.Model.Jeeps.State;
using Safari.Model.Map;
using Safari.Model.Movement;
using Safari.Model.Pathfinding;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Safari.Model.Jeeps
{
    public class Jeep : IMoving
    {
        public const int Capacity = 4;

        public event EventHandler? StateChanged;

        public Vector3 Position { get; set; }

        public MovementBehavior Movement;

        public VisitorManager VisitorManager { get; }

        public PathfindingHelper Pathfinding { get; }

        public State.State State { get; private set; }

        public Jeep(Vector3 vec3, VisitorManager visitorManager, PathfindingHelper pathfinding)
        {
            Position = vec3;
            Movement = new MovementBehavior(this, Position);
            VisitorManager = visitorManager;
            Pathfinding = pathfinding;
            State = new Idling(this);
        }

        internal void SetState(State.State state)
        {
            State.OnExit();
            State = state;
            State.OnEnter();
            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ModelUpdate(float elapsedTimeSec, int speedFactor) 
        {
            State.Update(elapsedTimeSec, speedFactor);
        }
    }
}
