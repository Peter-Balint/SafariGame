using Safari.Model.Animals;
using Safari.Model.Map;
using Safari.View.Animals;
using UnityEngine;

namespace Safari.View.UI.Animals
{
    [CreateAssetMenu(fileName = "WolfShopItem", menuName = "Configurations/WolfShopItem")]
    public class WolfShopItem : AnimalShopItem
    {
        public override Animal CreateAnimal(GridPosition gridPosition)
        {
            Wolf wolf = new Wolf(CollectionController.AnimalCollection.Pathfinding, Metadata, null);
            wolf.Position = CollectionController.GridPositionMapping[gridPosition];
            return wolf;
        }
    }
}
