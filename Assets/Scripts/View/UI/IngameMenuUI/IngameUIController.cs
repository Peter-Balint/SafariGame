using Safari.Model;
using TMPro;
using UnityEngine;
using UnityEngine.Diagnostics;

namespace Safari.View.UI
{
    public class IngameMenuController : MonoBehaviour
    {
        [SerializeField]
        private GameObject ConstructionMenu;
        [SerializeField]
        private GameObject AnimalShop;

        private MoneyManager MoneyManager;


        public TMP_Text TicketPriceText;


        void Start()
        {
            MoneyManager = SafariGame.Instance.MoneyManager;
            TicketPriceText.text = MoneyManager.ReadTicketPrice().ToString() + '$';
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                MoneyManager.CalculateVisitDesire();
            }
        }
        public void Open()
        {
            gameObject.SetActive(true);
        }
        public void OpenConstructionMenu()
        {
            ConstructionMenu.SetActive(true);
            gameObject.SetActive(false);
        }
        public void OpenAnimalShop()
        {
            AnimalShop.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnTicketPriceUp()
        {
            MoneyManager.RaiseTicketPrice();
            Debug.Log(MoneyManager.ReadTicketPrice());
            TicketPriceText.text = MoneyManager.ReadTicketPrice().ToString() + '$';

        }
        public void OnTicketPriceDown()
        {
            MoneyManager.LowerTicketPrice();
            Debug.Log(MoneyManager.ReadTicketPrice());
            TicketPriceText.text = MoneyManager.ReadTicketPrice().ToString() + '$';

        }
    }
}
