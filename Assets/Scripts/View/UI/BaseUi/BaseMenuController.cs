using UnityEngine;

namespace Safari.View
{
    public class BaseMenuController : MonoBehaviour
    {
        [SerializeField]
        private GameObject ConstructionMenu;
        
        void Start()
        {
        
        }

        
        void Update()
        {
        
        }

        public void OnConstructionMenuClicked()
        {
            ConstructionMenu.SetActive(true);
        }
    }
}
