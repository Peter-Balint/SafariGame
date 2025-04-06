using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Animals.State
{
    public class Wandering : Animals.State.State
    {
        public Wandering(Animal owner,int thirst) : base(owner, thirst)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
        }
    }
}
