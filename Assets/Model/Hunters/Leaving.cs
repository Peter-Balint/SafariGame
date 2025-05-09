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
            GridMovementCommand movementCommand = new GridMovementCommand(map.ExitCoords);
            movementCommand.Finished += (sender, args) => owner.Kill();
            owner.Movement.ExecuteMovement(movementCommand);
        }
    }
}
