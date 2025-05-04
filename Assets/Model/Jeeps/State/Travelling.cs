using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Jeeps.State
{
    public class Travelling : State
    {
        public Travelling(Jeep owner) : base(owner)
        {
        }
    }
}
