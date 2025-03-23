using Safari.Model.Animals;
using Safari.Model.Map;
using System.Collections.Generic;
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

        private Dictionary<GridPosition, Vector3> gridPositionMapping;

        public void Init(Animal animal, Vector3Int position, Dictionary<GridPosition, Vector3> gridPosMapping)
        {
            Position = position;
            gridPositionMapping = gridPosMapping;
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

            var animalMovement = displayed.GetComponent<AnimalMovement>();
            animalMovement.Init(animal.Movement, gridPositionMapping);
        }
        

        void Start()
        {
        
        }

        void Update()
        {
        
        }
    }
}
