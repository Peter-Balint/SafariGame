using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Animals.State
{
    public class Reuniting : State
    {
        public Reuniting(Animal owner, float thirst, float hunger) : base(owner, thirst, hunger)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
        }
    }
}
