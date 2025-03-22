using Safari.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.View.UI.Construction
{
    [CreateAssetMenu(fileName = "RoadShopItem", menuName = "Configurations/RoadShopItem")]
    public class RoadShopItem : BuildingShopItem
    {
        public override Field CreateField()
        {
            return new Road();
        }
    }
}
