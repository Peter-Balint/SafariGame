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
        public static FollowPreyMovementCommand SearchAndStalk(float finishDistance)
        {
            return new FollowPreyMovementCommand(finishDistance, true);
        }

        public static FollowPreyMovementCommand Chase(float finishDistance)
        {
            return new FollowPreyMovementCommand(finishDistance, false);
        }

        public float? FinishDistance { get; private set; }

        public bool SearchPrey { get; private set; }

        protected FollowPreyMovementCommand(float? finishDistance, bool searchPrey)
        {
            FinishDistance = finishDistance;
            SearchPrey = searchPrey;
        }

        public void ReportPreyNotFound()
        {
            ReportFailed(new PreyNotFound());
        }

        public void ReportPreyApproached(PreyApproached success)
        {
            ReportFinished(success);
        }
    }
}
