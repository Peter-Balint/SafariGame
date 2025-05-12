using Safari.Model.Animals;
using Safari.Model.Map;
using Safari.Model.Pathfinding;
using Safari.View.Animals;
using UnityEngine;

namespace Safari.View.UI.Animals
{
    public abstract class AnimalShopItem : ScriptableObject, IAnimalFactory
    {
        public Sprite Icon;

        public AnimalMetadata Metadata;

        public abstract Animal CreateAnimal(Vector3 Position, PathfindingHelper pathfinding, AnimalCollection collection);
    }
}
