using Safari.Model.Animals;
using Safari.Model.Map;
using Safari.View.Animals;
using UnityEngine;

namespace Safari.View.UI.Animals
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
