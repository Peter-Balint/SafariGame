#nullable enable
using Safari.Model.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.Model.Construction
{
    public class ConstructionManager
    {
        private Map.Map map;
		private MoneyManager moneyManager;

		public ConstructionManager(Map.Map map, MoneyManager moneym)
        {
            this.map = map;
			moneyManager = moneym;
		}

        public void BuildAt(Field field, GridPosition position) 
        {
            Field fieldAtPos = map.FieldAt(position);
            if (!fieldAtPos.CanPlaceHere(field))
            {
                return;
            }
			// TODO: handle money
			if (moneyManager.CanBuy(field.Metadata.Price))
			{
				Debug.Log(moneyManager.ReadBalance());
				map.ChangeFieldAt(position, field);
				moneyManager.AddToBalance(-field.Metadata.Price);
				Debug.Log(moneyManager.ReadBalance());
			}

		}
	}
}
