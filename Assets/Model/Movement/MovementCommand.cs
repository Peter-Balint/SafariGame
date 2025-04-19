#nullable enable
using System;

namespace Safari.Model.Movement
{
    public abstract class MovementCommand
    {
        public event EventHandler? Cancelled;

        public event EventHandler<MovementFinishedEventArgs>? Finished;

        public void Cancel()
        {
            Cancelled?.Invoke(this, EventArgs.Empty);
            Cancelled = null;
            Finished = null;
        }

        public void ReportFinished()
        {
            Finished?.Invoke(this, MovementFinishedEventArgs.Success);
            Cancelled = null;
            Finished = null;
        }

        public void ReportFailed(Failed failed)
        {
            Finished?.Invoke(this, new MovementFinishedEventArgs(failed));
            Cancelled = null;
            Finished = null;
        }
    }
}