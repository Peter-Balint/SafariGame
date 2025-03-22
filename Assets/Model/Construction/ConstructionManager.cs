#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Construction
{
    public class ConstructionManager
    {
        private Map map;

        public ConstructionManager(Map map)
        {
            this.map = map;
        }

        public void BuildAt(Field field, GridPosition position) 
        {
            Field fieldAtPos = map.FieldAt(position);
            if (!fieldAtPos.CanPlaceHere(field))
            {
                return;
            }
            // TODO: handle money
            map.ChangeFieldAt(position, field);
        }
    }
}
