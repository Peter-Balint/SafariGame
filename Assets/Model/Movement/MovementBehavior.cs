#nullable enable
using Safari.Model.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Movement
{
    public class MovementBehavior
    {
        public event EventHandler<MovementCommand>? CommandStarted;

        public MovementCommand? CurrentCommand;

        public GridPosition Location { get; private set; }

        public void MoveToGrid(GridPosition target)
        {
            CurrentCommand?.Cancel();
            CurrentCommand = new MovementCommand(target);
            CommandStarted?.Invoke(this, CurrentCommand);
            CurrentCommand.Cancelled += OnCommandCancelled; 
        }

        public void ReportLocation(GridPosition location)
        {
            Location = location;
        }

        private void OnCommandCancelled(object sender, EventArgs e)
        {
            if (sender == CurrentCommand)
            {
                CurrentCommand = null;
            }
        }
    }
}
