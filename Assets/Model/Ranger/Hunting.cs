using Safari.Model.Animals;
using Safari.Model.Map;
using Safari.Model.Movement;

namespace Safari.Model.Rangers
{
    public class Hunting : State
    {
        public Hunting(Ranger owner) : base(owner) 
        {
        }

        public override void Update(GridPosition target)
        {
            Field targetField = map.FieldAt(target);
            if (!(targetField is Water) && map.IsValidPosition(target)) 
            {
                owner.Movement.CurrentCommand?.Cancel();
                GridMovementCommand movementCommand = new GridMovementCommand(target);
                owner.Movement.ExecuteMovement(movementCommand);
            }
        }
    }
}
