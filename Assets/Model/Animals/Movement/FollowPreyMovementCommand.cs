#nullable enable
using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Animals.Movement
{
    public partial class FollowPreyMovementCommand : MovementCommand
    {
        public event EventHandler<StalkingFinishedEventArgs>? StalkingFinished;

        public float StalkingFinishedRadius { get; private set; }

        public float FinishedRadius { get; private set; }

        private bool stalkingFinished;

        public FollowPreyMovementCommand(float stalkingFinishedRadius, float finishedRadius)
        {
            StalkingFinishedRadius = stalkingFinishedRadius;
            FinishedRadius = finishedRadius;
        }

        public void ReportPreyNotFound()
        {
            OnStalkingFinished(new StalkingFinishedEventArgs(new PreyNotFound()));
            ReportFinished();
        }

        public void ReportPreyApproached(PreyApproached result)
        {

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
    }
}
