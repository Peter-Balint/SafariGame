using Safari.Model.Animals;
using Safari.Model.Map;
using Safari.View.Animals;
using UnityEngine;

namespace Safari.View.UI.Animals
{
    [CreateAssetMenu(fileName = "LionShopItem", menuName = "Configurations/LionShopItem")]
    public class LionShopItem : AnimalShopItem
    {
        public override Animal CreateAnimal(GridPosition gridPosition, AnimalCollectionController collectionController)
        {
            Lion lion = new Lion(collectionController.AnimalCollection.Pathfinding, Metadata, null);
            lion.Position = collectionController.GridPositionMapping[gridPosition];
            return lion;
        }
    }
}
