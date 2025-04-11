using Safari.Model.Animals;
using Safari.Model.Map;
using Safari.View.Animals;
using UnityEngine;

namespace Safari.View.UI.Animals
{
    [CreateAssetMenu(fileName = "SheepShopItem", menuName = "Configurations/SheepShopItem")]
    public class SheepShopItem : AnimalShopItem
    {
        public override Animal CreateAnimal(GridPosition gridPosition, AnimalCollectionController collectionController)
        {
            Sheep sheep = new Sheep(collectionController.AnimalCollection.Pathfinding, Metadata, null);
            sheep.Position = collectionController.GridPositionMapping[gridPosition];
            return sheep;
        }
    }
}
