using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Animals.Movement
{
    public class PreyApproached: Success
    {
        public IPrey Prey { get; private set; }

        public Chaser Chaser { get; private set; }

        public PreyApproached(IPrey prey, Chaser chaser)
        {
            Prey = prey;
            Chaser = chaser;
        }
    }
}
