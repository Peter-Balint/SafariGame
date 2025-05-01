using UnityEngine;
using Safari.Model;
using Safari.Model.Map;
using Safari.View.World.Map;
namespace Safari.View.Minimap
{
    public class MinimapNavigator : MonoBehaviour
    {
        public Camera MainCamera;

        private int mapSizeX;
        private int MapSizeZ;

        private void Start()
        {
            mapSizeX = SafariGame.Instance.Map.SizeX;
            MapSizeZ = SafariGame.Instance.Map.SizeZ;
        }

        public void Onclicked()
        {
            var localPosition = Input.mousePosition - new Vector3(Screen.width - 175, Screen.height - 175, 0);
            Debug.Log(localPosition);
            localPosition = new Vector3(localPosition.x / 160, localPosition.y / 160, 0);
            Debug.Log(localPosition);

            MainCamera.transform.position = new Vector3(localPosition.x * mapSizeX * mapSizeX /2, MainCamera.transform.position.y, localPosition.y * MapSizeZ * MapSizeZ /2 - MainCamera.transform.position.y );
        }

        
    }
}
