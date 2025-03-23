using Safari.Model.Animals;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

#nullable enable

namespace Safari.View.Animals
{
    public class AnimalDisplay : MonoBehaviour
    {
        public Animal? AnimalModel;

        private GameObject? displayed;

        public Vector3Int Position;

        [SerializeField]
        AnimalPrefabMapping mapping;


        public void Init(Animal animal, Vector3Int position)
        {
            Position = position;
            Trace.Assert(displayed == null);
            DisplayAnimal(animal);
        }

        public void DisplayAnimal(Animal animal)
        {
            AnimalModel = animal;
            if (displayed != null)
            {
                Destroy(displayed);
            }
            var prefab = mapping.GetPrefab(AnimalModel);
            if(prefab ==  null) { return; }
            displayed = Instantiate(prefab, transform, false);
        }
        

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
