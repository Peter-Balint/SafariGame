using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Animals.Movement
{
    public class ApproachMovementCommand: MovementCommand
    {
        public MovementBehavior Target { get; private set; }

        public ApproachMovementCommand(MovementBehavior target)
        {
            Target = target;
        }
    }
}
