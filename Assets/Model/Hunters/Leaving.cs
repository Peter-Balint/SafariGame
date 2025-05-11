using Safari.Model.Movement;
using UnityEngine;
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
            if (owner.Movement.Location.X == owner.Target.X && owner.Movement.Location.Z == owner.Target.Z)
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
