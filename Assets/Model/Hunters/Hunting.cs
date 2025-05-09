using Safari.Model.Map;
using Safari.Model.Movement;
using UnityEngine;

namespace Safari.Model.Hunters
{
    public class Hunting : State
    {
        public Hunting(Hunter owner) : base(owner)
        {
        }

        public override void Update(GridPosition target)
        {
            Field targetField = map.FieldAt(target);
            if (!(targetField is Water) && map.IsValidPosition(target))
            {
                GridMovementCommand movementCommand = new GridMovementCommand(target);
                owner.Movement.ExecuteMovement(movementCommand);
                Debug.Log($"Targeting animal at {target}");
            }
        }
    }
}
