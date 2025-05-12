using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Map
{
    public class Grass : Plant
    {
        public Grass(BuildingMetadata metadata) : base(metadata)
        {
        }

        public Grass() : this(BuildingMetadata.Default())
        {
        }
    }
}
