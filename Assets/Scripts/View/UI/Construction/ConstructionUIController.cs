#nullable enable
using Safari.Model;
using Safari.View.World.ConstructionGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.View.UI.Construction
{
    public class ConstructionUIController : MonoBehaviour
    {
        public ShopListingController ShopListingPrefab;

        public RectTransform ScrollViewContent;

        public BuildingShopItem[] ShopItems;

        public float ShopItemPadding;

        private ShopListingController? activeListing;

        private bool inBuildingMode = false;

        [SerializeField]
        private ConstructionGridController gridController;

        public void Open()
        {
            gameObject.SetActive(true);
        }


        public void Close()
        {
            gameObject.SetActive(false);
            activeListing = null;
            StopBuilding();

        }

        public void OnDemolish()
        {

        }

        private void Start()
        {
            float listingWidth = ShopListingPrefab.GetComponent<RectTransform>().rect.width;
            SetScrollViewWidth(listingWidth);

            for (int i = 0; i < ShopItems.Length; i++)
            {
                BuildingShopItem item = ShopItems[i];

                var listing = Instantiate(ShopListingPrefab);
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

        private void OnShopListingClick(ShopListingController listing)
        {
            if (activeListing != null)
            {
                activeListing.Selected = false;
            }

            if (listing == activeListing)
            {
                activeListing = null;
                StopBuilding();
            }
            else
            {
                activeListing = listing;
                listing.Selected = true;
                StartBuilding();
            }
        }

        private void StartBuilding()
        {
            if (inBuildingMode)
            {
                return;
            }
            inBuildingMode = true;
            gridController.Open();
            gridController.Click.AddListener(OnGridClick);
        }

        private void StopBuilding()
        {
            inBuildingMode = false;
            gridController.Close();
            gridController.Click.RemoveListener(OnGridClick);
        }

        private void OnGridClick(GridPosition position)
        {
            if (activeListing?.ShopItem == null)
            {
                return;
            }
            SafariGame.Instance.Construction.BuildAt(activeListing.ShopItem.CreateField(), position);
        }
    }
}
