using Safari.Model.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Rangers
{
    public class Hunting : State
    {
        Predator target;
        public Hunting(Ranger owner, Predator target) : base(owner) 
        {
            this.target = target;
        }

        public override void Update()
        {

        }
    }
}
