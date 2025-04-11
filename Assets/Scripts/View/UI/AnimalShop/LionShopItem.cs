using Safari.Model.Animals;
using Safari.Model.Map;
using UnityEngine;

namespace Safari.View.UI.Animals
{
    [CreateAssetMenu(fileName = "LionShopItem", menuName = "Configurations/LionShopItem")]
    public class LionShopItem : AnimalShopItem
    {
        public override Animal CreateAnimal(GridPosition gridPosition)
        {
            Lion lion = new Lion(CollectionController.AnimalCollection.Pathfinding, Metadata, null);
            lion.Position = CollectionController.GridPositionMapping[gridPosition];
            return lion;
        }
    }
}
