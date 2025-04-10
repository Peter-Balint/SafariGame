using Safari.Model.Map;
using Safari.Model.Animals;
using Safari.View.World.ConstructionGrid;
using System;
using UnityEngine;

namespace Safari.View.Animals
{
    public class AnimalShopController : MonoBehaviour
    {
        public AnimalCollectionController AnimalCollectionController;

        public AnimalListingController ListingPrefab;

        public RectTransform ScrollViewContent;

        public AnimalShopItem[] ShopItems;

        public float ShopItemPadding;

        private AnimalListingController? activeListing;

        private bool inCreationMode = false;

        [SerializeField]
        private ConstructionGridController gridController;

        public void Close()
        {
            gameObject.SetActive(false);
            activeListing = null;
            StopCreation();
        }


        void Start()
        {
            float listingWidth = ListingPrefab.GetComponent<RectTransform>().rect.width;
            SetScrollViewWidth(listingWidth);

            for (int i = 0; i < ShopItems.Length; i++)
            {
                AnimalShopItem item = ShopItems[i];

                var listing = Instantiate(ListingPrefab);
                listing.Init(item);

                RectTransform listingTransform = listing.GetComponent<RectTransform>();
                listingTransform.SetParent(ScrollViewContent, false);
                Vector3 pos = new Vector3(1, 0, 0) * (i * (listingWidth + 2 * ShopItemPadding) + ShopItemPadding);
                listingTransform.SetLocalPositionAndRotation(pos, Quaternion.identity);

                listing.Clicked.AddListener(() => OnShopListingClick(listing));
            }
        }


        private void SetScrollViewWidth(float listingWidth)
        {
            float scrollViewWidth = ShopItems.Length * (listingWidth + 2 * ShopItemPadding);
            ScrollViewContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scrollViewWidth);
        }
        private void OnShopListingClick(AnimalListingController listing)
        {
            if (activeListing != null)
            {
                activeListing.Selected = false;
            }

            if (listing == activeListing)
            {
                activeListing = null;
                StopCreation();
            }
            else
            {
                activeListing = listing;
                listing.Selected = true;
                StartCreation();
            }
        }

        private void StartCreation()
        {
            if(inCreationMode)
            {
                return;
            }
            inCreationMode = true;
            gridController.Open();
            gridController.Click.AddListener(OnGridClick);
        }
        private void StopCreation()
        {
            inCreationMode = false;
            activeListing = null;
            gridController.Close();
            gridController.Click.RemoveListener(OnGridClick);
        }

        private void OnGridClick(GridPosition position)
        {
            if (activeListing?.ShopItem == null)
            {
                return;
            }
            Animal animal = activeListing.ShopItem.CreateAnimal(position);
        }

    }
}
