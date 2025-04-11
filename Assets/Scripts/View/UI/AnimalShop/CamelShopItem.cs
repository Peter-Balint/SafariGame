using Safari.Model.Animals;
using Safari.Model.Map;
using Safari.View.Animals;
using UnityEngine;

namespace Safari.View.UI.Animals
{
    [CreateAssetMenu(fileName = "CamelShopItem", menuName = "Configurations/CamelShopItem")]
    public class CamelShopItem : AnimalShopItem
    {
        public override Animal CreateAnimal(GridPosition gridPosition, AnimalCollectionController collectionController)
        {
            Camel camel = new Camel(collectionController.AnimalCollection.Pathfinding, Metadata, null);
            camel.Position = collectionController.GridPositionMapping[gridPosition];
            return camel;
        }
    }
}
