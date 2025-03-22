using Safari.Model;
using Safari.View.UI.Construction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.View.UI.Construction
{
    [CreateAssetMenu(fileName = "WaterShopItem", menuName = "Configurations/WaterShopItem")]
    public class WaterShopItem : BuildingShopItem
    {
        public override Field CreateField()
        {
            return new Water(Metadata);
        }
    }
}
