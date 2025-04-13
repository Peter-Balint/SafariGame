using Safari.Model.Animals;
using Safari.Model.Map;
using Safari.Model.Pathfinding;
using Safari.View.Animals;
using UnityEngine;

namespace Safari.View.UI.Animals
{
    [CreateAssetMenu(fileName = "CamelShopItem", menuName = "Configurations/CamelShopItem")]
    public class CamelShopItem : AnimalShopItem
    {
        public override Animal CreateAnimal(Vector3 Position, PathfindingHelper pathfinding)
        {
            Camel camel = new Camel(pathfinding, Metadata, null);
            camel.Position = Position;
            return camel;
        }
    }
}
