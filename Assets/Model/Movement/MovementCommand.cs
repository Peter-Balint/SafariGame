#nullable enable
using Safari.Model.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Movement
{
    public class MovementCommand
    {
        public event EventHandler? Cancelled;
        public GridPosition TargetCell { get; private set; }

        public MovementCommand(GridPosition targetCell)
        {
            TargetCell = targetCell;
        }

        public void Cancel()
        {
            Cancelled?.Invoke(this, EventArgs.Empty);
        }
    }
}
