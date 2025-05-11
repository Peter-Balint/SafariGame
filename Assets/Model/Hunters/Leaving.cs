using Safari.Model.Movement;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

namespace Safari.Model.Hunters
{
    public class Leaving : State
    {
        public Leaving(Hunter owner) : base(owner) { }

        public override void OnEnter()
        {
            owner.Target = map.ExitCoords;
            GridMovementCommand movementCommand = new GridMovementCommand(map.ExitCoords);
            owner.Movement.ExecuteMovement(movementCommand);
        }

        public override void Update()
        {
            if (owner.Movement.Location.X == map.ExitCoords.X && owner.Movement.Location.Z == map.ExitCoords.Z)
            {
                OnTargetReached();
            }
        }

        private void OnTargetReached()
        {
            owner.Kill();
        }
    }
}
