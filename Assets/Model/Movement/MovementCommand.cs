#nullable enable
using System;

namespace Safari.Model.Movement
{
    public class MovementCommand
    {
        public event EventHandler? Cancelled;

        public event EventHandler? Finished;

        public void Cancel()
        {
            Cancelled?.Invoke(this, EventArgs.Empty);
            Cancelled = null;
            Finished = null;
        }

        public void ReportFinished()
        {
            Finished?.Invoke(this, EventArgs.Empty);
            Cancelled = null;
            Finished = null;
        }
    }
}