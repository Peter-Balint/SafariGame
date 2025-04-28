#nullable enable
using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace Safari.Model.Animals.Movement
{
    public partial class FollowPreyMovementCommand : MovementCommand
    {
        public event EventHandler<StalkingFinishedEventArgs>? StalkingFinished;

        public event EventHandler? PreyEscaped;

        public float StalkingFinishedRadius { get; private set; }

        public float FinishedRadius { get; private set; }

        public float EscapeRadius { get; private set; }

        public bool IsStalkingFinished => stalkingFinished;

        public IPrey? Prey { get; private set; }

        public bool CanEscape { get; set; }

        private bool stalkingFinished;

        private bool preyEscaped;

        public FollowPreyMovementCommand(float stalkingFinishedRadius, float finishedRadius, float escapeRadius)
        {
            StalkingFinishedRadius = stalkingFinishedRadius;
            FinishedRadius = finishedRadius;
            EscapeRadius = escapeRadius;
        }

        public void ReportPreyNotFound()
        {
            OnStalkingFinished(new StalkingFinishedEventArgs(new PreyNotFound()));
            ReportFinished();
        }

        public void ReportPreyApproached(PreyApproached result)
        {
            Prey = result.Prey;
            OnStalkingFinished(new StalkingFinishedEventArgs(result));
        }

        private void OnStalkingFinished(StalkingFinishedEventArgs e)
        {
            if (stalkingFinished)
            {
                return;
            }
            stalkingFinished = true;
            StalkingFinished?.Invoke(this, e);
        }

        public void ReportPreyEscaped()
        {
            if (preyEscaped)
            {
                return;
            }
            preyEscaped = true;
            PreyEscaped?.Invoke(this, EventArgs.Empty);
        }

        public override void Cancel()
        {
            base.Cancel();
            Prey?.OnEscaped();
        }
    }
}
