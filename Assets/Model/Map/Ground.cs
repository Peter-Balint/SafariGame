using Safari.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Map
{
    public class Ground : Field
    {
        public override bool CanDemolish => true;

        public Ground() : base(BuildingMetadata.Default())
        {
        }

        public override bool CanPlaceHere(Field field)
        {
            return field switch
            {
                Plant => true,
                Road => true,
                Water => true,
                _ => false,
            };
        }

    }
}
