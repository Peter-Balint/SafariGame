using Safari.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.View.UI.Construction
{
    public abstract class BuildingShopItem : ScriptableObject
    {
        public Sprite Icon;

        public BuildingMetadata Metadata;

        public abstract Field CreateField();
    }
}
