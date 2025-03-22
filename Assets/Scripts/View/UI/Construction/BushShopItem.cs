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
    [CreateAssetMenu(fileName = "BushShopItem", menuName = "Configurations/BushShopItem")]
    public class BushShopItem : BuildingShopItem
    {
        public override Field CreateField()
        {
            return new Bush(Metadata);
        }
    }
}
