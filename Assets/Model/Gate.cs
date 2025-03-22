using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model
{
    public class Gate : Field
    {
        public override bool CanDemolish => false;

        public Gate() : base(BuildingMetadata.Default())
        {
        }

        public override bool CanPlaceHere(Field field)
        {
            return false;
        }

    }
}
