using UnityEngine;

namespace Safari.View.Minimap
{
    public class MinimapComponent : MonoBehaviour
    {
        //script adding minimap icons to prefabs that didn't previously possess a script

        public GameObject miniMapIcon;

        void Start()
        {
            Instantiate(miniMapIcon, transform, false);
        }

    }
}
