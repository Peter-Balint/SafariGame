using Safari.Model.Map;
using Safari.Model.Movement;
using System;
using UnityEngine;

namespace Safari.Model.Hunters
{
    public class Hunter : IMoving
    {
        public Vector3 Position { get; set; }

        public MovementBehavior Movement;

        public event EventHandler? Died;
        public event EventHandler? EnteredLeaving;

        private GridPosition target;
        public GridPosition Target
        {
            get { return target; }
            set
            {
                target = value;
                state.OnTargetChanged();
            }
        }

        private State state;

        [SerializeField]
        private float shoootingRange = 90;

        public Hunter()
        {
            Movement = new MovementBehavior(this, Vector3.zero);
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
            else if(this.state is Leaving)
            {
                EnteredLeaving?.Invoke(this, EventArgs.Empty);
            }
        }

        public void ModelUpdate()
        {
            state.Update();
        }

        public void Kill()
        {
            SetState(new Dead(this));
        }
        
        public bool CheckInShootingDistance(Vector3 hunterVector, Vector3 targetVector)
        {
            return (hunterVector - targetVector).sqrMagnitude <= shoootingRange * shoootingRange;
        }
    }
}
