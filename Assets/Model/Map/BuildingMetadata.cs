using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Map
{
    [Serializable]
    public class BuildingMetadata
    {
        public static BuildingMetadata Default()
        {
            return new BuildingMetadata();
        }

        public int Price;

        public int Value;
    }
}
