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
        }

        public void ModelUpdate()
        {

        }
        public void ModelUpdate(GridPosition target)
        {
            state.Update(target);
        }

        public void Kill()
        {
            SetState(new Dead(this));
        }

        public bool CheckInShootingDistance(Vector3 rangerVector, Vector3 targetVector)
        {
            return (rangerVector - targetVector).sqrMagnitude <= shoootingRange * shoootingRange;
        }
    }
}
