using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model
{
    public class Water : Field
    {
        public override bool CanDemolish => true;

        public override bool CanPlaceHere(Ground field)
        {
            return false;
        }

        public override bool CanPlaceHere(Plant field)
        {
            return false;
        }

        public override bool CanPlaceHere(Road field)
        {
            return false;
        }

        public override bool CanPlaceHere(Water field)
        {
            return false;
        }

        public override bool CanPlaceHere(Gate field)
        {
            return false;
        }
    }
}
