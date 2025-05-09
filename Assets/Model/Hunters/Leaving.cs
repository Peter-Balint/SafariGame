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
            owner.Movement.ExecuteMovement(movementCommand);
        }
    }
}
