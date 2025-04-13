using Safari.Model.Animals;
using Safari.Model.Map;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

#nullable enable

namespace Safari.View.Animals
{
    public class AnimalDisplay : MonoBehaviour
    {
        public Animal? AnimalModel;

        private GameObject? displayed;

        public Vector3 Position;

        [SerializeField]
        AnimalPrefabMapping mapping;

        private float a = 0;

        private Dictionary<GridPosition, Vector3> gridPositionMapping;

        public void Init(Animal animal, Vector3 position, Dictionary<GridPosition, Vector3> gridPosMapping)
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
            var navMeshAgent = this.GetComponent<NavMeshAgent>();
            animalMovement.Init(animal.Movement, navMeshAgent, gridPositionMapping);
        }
        

        void Start()
        {
            
        }

        public void Update()
        {
            AnimalModel?.ModelUpdate(Time.deltaTime);
        }
    }
}
