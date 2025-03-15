using UnityEngine;
using UnityEngine.Events;

namespace Safari.View.World.BuilderGrid
{
    public class BuilderCell : MonoBehaviour
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

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

            meshRenderer = GetComponentInChildren<MeshRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void OnHoverEnter()
        {
            isHovered = true;
            ChangeMaterial();
        }

        public void OnHoverLeave()
        {
            isHovered = false;
            ChangeMaterial();
        }

        public void OnClick()
        {
            if (enabled)
            {
                Click?.Invoke();
            }
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
