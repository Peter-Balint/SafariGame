using UnityEngine;
using UnityEngine.Events;

namespace Safari.View.World.ConstructionGrid
{
    public class ConstructionCell : MonoBehaviour
    {

        public bool IsForbidden
        {
            get => isForbidden;
            set
            {
                isForbidden = value;
                ChangeMaterial();
            }
        }

        public Material Default;

        public Material Hover;

        public Material Forbidden;

        public Material ForbiddenHover;

        public UnityEvent Click;

        private MeshRenderer meshRenderer;

        private bool isHovered = false;

        private bool isForbidden;

        public void OnMouseDown()
        {
            if (enabled)
            {
                Click?.Invoke();
            }
        }

        public void OnMouseEnter()
        {
            isHovered = true;
            ChangeMaterial();
        }

        public void OnMouseExit()
        {
            isHovered = false;
            ChangeMaterial();
        }

        private void Start()
        {
            meshRenderer = GetComponentInChildren<MeshRenderer>();
        }

        private void ChangeMaterial()
        {
            Material material;
            if (isHovered)
            {
                if (isForbidden)
                {
                    material = ForbiddenHover;
                }
                else
                {
                    material = Hover;
                }
            }
            else
            {
                if (isForbidden)
                {
                    material = Forbidden;
                }
                else
                {
                    material = Default;
                }
            }

            if (material == meshRenderer.material)
            {
                return;
            }

            meshRenderer.material = material;
        }
    }
}
