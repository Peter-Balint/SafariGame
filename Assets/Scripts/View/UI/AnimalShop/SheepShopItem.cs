using Safari.Model.Animals;
using Safari.Model.Map;
using UnityEngine;

namespace Safari.View.UI.Animals
{
    [CreateAssetMenu(fileName = "SheepShopItem", menuName = "Configurations/SheepShopItem")]
    public class SheepShopItem : AnimalShopItem
    {
        public override Animal CreateAnimal(GridPosition gridPosition)
        {
            Sheep sheep = new Sheep(CollectionController.AnimalCollection.Pathfinding, Metadata, null);
            sheep.Position = CollectionController.GridPositionMapping[gridPosition];
            return sheep;
        }
    }
}
