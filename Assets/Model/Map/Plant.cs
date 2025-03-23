using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Map
{
    public abstract class Plant : Field
    {
        public override bool CanDemolish => true;

        protected Plant(BuildingMetadata metadata) : base(metadata)
        {
        }

        public override bool CanPlaceHere(Field field)
        {
            return false;
        }
    }
}
