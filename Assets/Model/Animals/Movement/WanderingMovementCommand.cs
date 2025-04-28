using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Animals.Movement
{
    public class WanderingMovementCommand : MovementCommand
    {
        public WanderingMovementCommand? Followed { get; private set; }

        public WanderingMovementCommand(WanderingMovementCommand? followed) : base()
        {
            Followed = followed;
        }

        public WanderingMovementCommand(): base()
        {
            
        }

    }
}
