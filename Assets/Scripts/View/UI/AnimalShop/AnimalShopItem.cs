using Safari.Model.Animals;
using Safari.Model.Map;
using Safari.Model.Pathfinding;
using UnityEngine;

namespace Safari.View.Animals
{
    //[CreateAssetMenu(fileName = "AnimalShopItem", menuName = "Scriptable Objects/AnimalShopItem")]
    public abstract class AnimalShopItem : ScriptableObject
    {
        public Sprite Icon;

        public AnimalMetadata Metadata;

        public AnimalCollectionController CollectionController;
        public abstract Animal CreateAnimal(GridPosition position);
    }
}
