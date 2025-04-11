using Safari.Model.Animals;
using Safari.Model.Map;
using Safari.View.Animals;
using UnityEngine;

namespace Safari.View.UI.Animals
{
    [CreateAssetMenu(fileName = "WolfShopItem", menuName = "Configurations/WolfShopItem")]
    public class WolfShopItem : AnimalShopItem
    {
        public override Animal CreateAnimal(GridPosition gridPosition, AnimalCollectionController collectionController)
        {
            Wolf wolf = new Wolf(collectionController.AnimalCollection.Pathfinding, Metadata, null);
            wolf.Position = collectionController.GridPositionMapping[gridPosition];
            return wolf;
        }
    }
}
