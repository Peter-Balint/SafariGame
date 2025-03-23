using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Map
{
    public class Road : Field
    {
        public override bool CanDemolish => true;

        public Road(BuildingMetadata metadata) : base(metadata)
        {
        }

        public override bool CanPlaceHere(Field field)
        {
            return false;
        }
    }
}
