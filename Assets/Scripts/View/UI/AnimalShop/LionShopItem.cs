using Safari.Model.Animals;
using Safari.Model.Map;
using Safari.Model.Pathfinding;
using Safari.View.Animals;
using UnityEngine;

namespace Safari.View.UI.Animals
{
    [CreateAssetMenu(fileName = "LionShopItem", menuName = "Configurations/LionShopItem")]
    public class LionShopItem : AnimalShopItem
    {
        public override Animal CreateAnimal(Vector3 Position, PathfindingHelper pathfinding)
        {
            Lion lion = new Lion(pathfinding, Metadata, null);
            lion.Position = Position;
            return lion;
        }
    }
}
