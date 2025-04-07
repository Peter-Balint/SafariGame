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

            public static Offset TopLeftCorner { get; internal set; } = new Offset(-1, -1);

            public static Offset TopRightCorner { get; internal set; } = new Offset(1, -1);

            public static Offset BottomLeftCorner { get; internal set; } = new Offset(-1, 1);

            public static Offset BottomRightCorner { get; internal set; } = new Offset(1, 1);

            public static Offset TopSide(float x) => new Offset(x, -1);

            public static Offset LeftSide(float z) => new Offset(-1, z);

            public static Offset RightSide(float z) => new Offset(1, z);

            public static Offset BottomSide(float x) => new Offset(x, 1);


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
