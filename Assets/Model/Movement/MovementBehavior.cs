#nullable enable
using Safari.Model.Animals;
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

        public event EventHandler<GridPosition>? GridPositionChanged;


        public MovementCommand? CurrentCommand { get; private set; }

        public GridPosition Location { get; private set; }

        public IMoving Owner { get; private set; }

        public MovementBehavior(IMoving entity) 
        {
            Owner = entity;
        }

        public void ExecuteMovement(MovementCommand command)
        {
            CurrentCommand?.Cancel();
            CurrentCommand = command;
            CommandStarted?.Invoke(this, CurrentCommand);
            CurrentCommand.Cancelled += OnCommandCancelled;
            CurrentCommand.Finished += OnCommandFinished;
        }

        public void ReportLocation(GridPosition location)
        {
            Location = location;
            GridPositionChanged?.Invoke(this, Location);
        }

        public void AbortMovement()
        {
            CurrentCommand?.Cancel();
            CurrentCommand = null;
        }

        private void OnCommandCancelled(object sender, EventArgs e)
        {
            if (sender == CurrentCommand)
            {
                CurrentCommand = null;
            }
        }

        private void OnCommandFinished(object sender, EventArgs e)
        {
            if (sender == CurrentCommand)
            {
                CurrentCommand = null;
            }
        }
    }
}
