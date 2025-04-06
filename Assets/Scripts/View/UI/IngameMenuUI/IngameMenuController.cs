using UnityEngine;

namespace Safari.View
{
    public class IngameMenuController : MonoBehaviour
    {
        [SerializeField]
        private GameObject ConstructionMenu;
        
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
    }
}
