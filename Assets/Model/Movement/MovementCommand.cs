#nullable enable
using System;

namespace Safari.Model.Movement
{
    public abstract class MovementCommand
    {
        public event EventHandler? Cancelled;

        public event EventHandler? Finished;

        public object? Extra { get; set; }

        public virtual void Cancel()
        {
            Cancelled?.Invoke(this, EventArgs.Empty);
            Cancelled = null;
            Finished = null;
        }

        public virtual void ReportFinished()
        {
            Finished?.Invoke(this, EventArgs.Empty);
            Cancelled = null;
            Finished = null;
        }
    }
}