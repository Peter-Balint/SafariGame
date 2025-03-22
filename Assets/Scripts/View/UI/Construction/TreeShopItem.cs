using Safari.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.View.UI.Construction
{
    [CreateAssetMenu(fileName = "TreeShopItem", menuName = "Configurations/TreeShopItem")]
    public class TreeShopItem : BuildingShopItem
    {
        public override Field CreateField()
        {
            return new Model.Tree();
        }
    }
}
