using UnityEngine;
using UnityEngine.UIElements;
namespace Safari.View.Minimap
{
    public class MinimapNavigator : MonoBehaviour
    {
        public Camera MainCamera;


        void Start()
        {
            //MainCamera.transform.position += new Vector3(-100, 0, -100);
        }

        /*private void OnMouseEnter()
        {
            Debug.Log(Input.mousePosition);
        }
        private void OnMouseDown()
        {
            Debug.Log(Input.mousePosition);
        }*/
        public void Onclicked()
        {
            var localPosition = Input.mousePosition - new Vector3(Screen.width - 175, Screen.height - 175, 0);
            Debug.Log(localPosition);
            localPosition = new Vector3(localPosition.x / 160, localPosition.y / 160, 0);
            Debug.Log(localPosition);
        }
    }
}
