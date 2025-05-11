using Safari.Model.Map;
using Safari.Model.Movement;
using System;
using UnityEngine;

namespace Safari.Model.Hunters
{
    public class Hunting : State
    {
        public Hunting(Hunter owner) : base(owner)
        {
        }

        public override void OnTargetChanged()
        {
            Field targetField = map.FieldAt(owner.Target);
            if (!(targetField is Water) && map.IsValidPosition(owner.Target))
            {
                GridMovementCommand movementCommand = new GridMovementCommand(owner.Target);
                //movementCommand.Finished += OnTargetReached;
                owner.Movement.ExecuteMovement(movementCommand);
                Debug.Log($"Targeting animal at {owner.Target.X},{owner.Target.Z}");
            }
        }

        public override void Update()
        {
            if(owner.Movement.Location.X == owner.Target.X && owner.Movement.Location.Z == owner.Target.Z)
            {
                OnTargetReached();
            }
        }

        private void OnTargetReached()
        {
            TransitionTo(new Leaving(owner));
            Debug.Log("leaving");
        }
    }
}
