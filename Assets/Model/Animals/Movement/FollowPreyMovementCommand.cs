using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Animals.Movement
{
    public class FollowPreyMovementCommand : MovementCommand
    {
        public float? FinishDistance { get; private set; }

        public FollowPreyMovementCommand(float? finishDistance)
        {
            FinishDistance = finishDistance;
        }

        public FollowPreyMovementCommand()
        {
            FinishDistance = null;
        }

        public void ReportPreyNotFound()
        {
            ReportFailed(new PreyNotFound());
        }
    }
}
