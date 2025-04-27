    using Safari.Model.Animals;
using Safari.Model.GameSpeed;
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

        //public Vector3 Position;
        //position is unused here?

        [SerializeField]
        AnimalPrefabMapping mapping;

        private Dictionary<GridPosition, Vector3> gridPositionMapping;

        private GameSpeedManager gameSpeedManager;

        public void Init(Animal animal, Vector3 position, Dictionary<GridPosition, Vector3> gridPosMapping, GameSpeedManager gameSpeedManager)
        {
            //Position = position;
            gridPositionMapping = gridPosMapping;
            Trace.Assert(displayed == null);
            DisplayAnimal(animal, gameSpeedManager);
        }

        public void DisplayAnimal(Animal animal, GameSpeedManager gameSpeedManager)
        {
            AnimalModel = animal;
            if (displayed != null)
            {
                Destroy(displayed);
            }
            var prefab = mapping.GetPrefab(AnimalModel);
            if(prefab ==  null) { return; }
            
            displayed = Instantiate(prefab, transform, false);
            
            this.gameSpeedManager = gameSpeedManager;

            var animalMovement = displayed.GetComponent<AnimalMovement>();
            var navMeshAgent = this.GetComponent<NavMeshAgent>();
            animalMovement.Init(animal.Movement, navMeshAgent, gridPositionMapping, gameSpeedManager);
        }
        

        void Start()
        {
            
        }

        public void Update()
        {
            AnimalModel?.ModelUpdate(Time.deltaTime, gameSpeedManager.CurrentSpeedToNum());
        }
    }
}
