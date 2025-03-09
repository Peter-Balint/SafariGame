using UnityEngine;
using Safari.Model;

namespace Safari.View.World
{
    public class MapDisplay : MonoBehaviour
    {
        public FieldDisplay FieldDisplayPrefab;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            BuildMap();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void BuildMap()
        {
            for (int x = 0; x < SafariGame.Instance.Map.SizeY; x++)
            {
                for (int y = 0; y < SafariGame.Instance.Map.SizeY; y++)
                {
                    var display = Instantiate(
                        FieldDisplayPrefab,
                        new Vector3(),
                        Quaternion.identity,
                        new InstantiateParameters()
                        {
                            parent = transform,
                            worldSpace = false
                        });


                }
            }
        }
    }

}

