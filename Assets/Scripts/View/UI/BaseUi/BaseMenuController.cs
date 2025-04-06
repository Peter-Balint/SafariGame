using UnityEngine;

namespace Safari.View
{
    public class BaseMenuController : MonoBehaviour
    {
        [SerializeField]
        private GameObject ConstructionMenu;
        [SerializeField]
        private GameObject AnimalShop;
        
        void Start()
        {
        
        }

        
        void Update()
        {
        
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
    }
}
