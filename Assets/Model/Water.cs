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

        public Water(BuildingMetadata metadata) : base(metadata)
        {
        }

        public override bool CanPlaceHere(Field field)
        {
            return false;
        }
    }
}
