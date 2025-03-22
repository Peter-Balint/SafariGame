using Safari.Model.Animals;
using UnityEngine;

#nullable enable

namespace Safari.View
{
    public class AnimalDisplay : MonoBehaviour
    {
        private Animal? animalModel;

        private GameObject? displayed;

        public Vector3Int Position;

        [SerializeField]
        AnimalPrefabMapping mapping;

        

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
