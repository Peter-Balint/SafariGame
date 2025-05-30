using Safari.Model.Animals;
using Safari.Model.Map;
using Safari.Model.Pathfinding;
using Safari.View.Animals;
using UnityEngine;

namespace Safari.View.UI.Animals
{
    [CreateAssetMenu(fileName = "WolfShopItem", menuName = "Configurations/WolfShopItem")]
    public class WolfShopItem : AnimalShopItem
    {
        public override Animal CreateAnimal(Vector3 Position, PathfindingHelper pathfinding, AnimalCollection collection)
        {
            Wolf wolf = new Wolf(pathfinding, Metadata, null, collection, Position);
            return wolf;
        }
    }
}
