using Safari.Model.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.View.UI.Construction
{
    [CreateAssetMenu(fileName = "GrassShopItem", menuName = "Configurations/GrassShopItem")]
    public class GrassShopItem : BuildingShopItem
    {
        public override Field CreateField()
        {
            return new Grass(Metadata);
        }
    }
}
