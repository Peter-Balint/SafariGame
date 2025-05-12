#nullable enable
using Safari.Model.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Movement
{
    public class GridMovementCommand : MovementCommand
    {
        public struct Offset
        {
            public static Offset Zero { get; internal set; } = new Offset(0, 0);

            public float DeltaX { get; private set; }

            public float DeltaZ { get; private set; }

            public Offset(float deltaX, float deltaZ)
            {
                DeltaX = deltaX;
                DeltaZ = deltaZ;
            }
        }

        public GridPosition TargetCell { get; private set; }

        public Offset TargetOffset { get; private set; }

        public GridMovementCommand(GridPosition targetCell, Offset targetOffset)
        {
            TargetCell = targetCell;
            TargetOffset = targetOffset;
        }

        public GridMovementCommand(GridPosition targetCell)
        {
            TargetCell = targetCell;
            TargetOffset = Offset.Zero;
        }
    }
}
