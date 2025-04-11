using Safari.Model.Animals;
using Safari.Model.Map;
using Safari.Model.Pathfinding;
using UnityEngine;

namespace Safari.View.UI.Animals
{
    [CreateAssetMenu(fileName = "CamelShopItem", menuName = "Configurations/CamelShopItem")]
    public class CamelShopItem : AnimalShopItem
    {
        public override Animal CreateAnimal(GridPosition gridPosition)
        {
            Camel camel = new Camel(CollectionController.AnimalCollection.Pathfinding, Metadata, null);
            camel.Position = CollectionController.GridPositionMapping[gridPosition];
            return camel;
        }
    }
}
