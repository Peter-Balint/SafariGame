using Safari.Model.Animals;
using Safari.Model.Map;
using Safari.Model.Pathfinding;
using Safari.View.Animals;
using UnityEngine;

namespace Safari.View.UI.Animals
{
    [CreateAssetMenu(fileName = "SheepShopItem", menuName = "Configurations/SheepShopItem")]
    public class SheepShopItem : AnimalShopItem
    {
        public override Animal CreateAnimal(Vector3 Position, PathfindingHelper pathfinding, AnimalCollection collection)
        {
            Sheep sheep = new Sheep(pathfinding, Metadata, null, collection, Position);
            return sheep;
        }
    }
}
