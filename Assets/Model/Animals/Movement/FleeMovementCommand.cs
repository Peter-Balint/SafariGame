using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Animals.Movement
{
    public class FleeMovementCommand : MovementCommand
    {
        public Chaser Chaser { get; private set; }

        public FleeMovementCommand(Chaser chaser)
        {
            Chaser = chaser;
        }
    }
}
