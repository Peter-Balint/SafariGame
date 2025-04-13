using Safari.View.UI.Construction;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Safari.View.UI.Animals
{
    public class AnimalListingController : MonoBehaviour
    {
        public bool Selected
        {
            get => selected;
            set
            {
                if (value == selected) { return; }
                selected = value;
                OnSelectedChanged();
            }
        }

        public TMP_Text Price;

        public Image Icon;

        public Image Wrapper;

        public AnimalShopItem? ShopItem { get; private set; }

        public UnityEvent Clicked;

        private bool selected;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Init(AnimalShopItem item)
        {
            ShopItem = item;
            Price.text = item.Metadata.Price + "$";
            Icon.sprite = item.Icon;
        }

        public void OnClick()
        {
            Clicked?.Invoke();
        }

        private void OnSelectedChanged()
        {
            Wrapper.enabled = selected;
        }
    }
}

