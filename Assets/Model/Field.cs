using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model
{
    public abstract class Field
    {
        public abstract bool CanDemolish { get; }

        public abstract bool CanPlaceHere(Field field);

        public BuildingMetadata Metadata { get; }

        protected Field(BuildingMetadata metadata)
        {
            Metadata = metadata;
        }
    }
}
