using UnityEngine;
using Safari.Model.Rangers;
using Safari.Model.Map;
using System.Collections.Generic;
using System.Diagnostics;
using Safari.View.Animals;
using Safari.Model.Animals;
using Safari.Model.GameSpeed;
using UnityEngine.AI;
using System.Runtime.CompilerServices;

namespace Safari.View.Rangers
{
    public class RangerDisplay : MonoBehaviour
    {
        public Ranger? Ranger;

        public GameObject RangerPrefab;
        private GameObject? displayed;

        private GameSpeedManager gameSpeedManager;

        private Dictionary<GridPosition, Vector3> gridPositionMapping;

        public void Init(Ranger ranger, Dictionary<GridPosition, Vector3> gridPosMapping, GameSpeedManager gameSpeedManager)
        {
            gridPositionMapping = gridPosMapping;
            Trace.Assert(displayed == null);
            DisplayRanger(ranger, gameSpeedManager);
        }

        private void DisplayRanger(Ranger ranger, GameSpeedManager gameSpeedManager)
        {
            this.Ranger = ranger;

            if (RangerPrefab == null) { return; }
            displayed = Instantiate(RangerPrefab, transform, false);

            this.gameSpeedManager = gameSpeedManager;
            var rangerMovement = displayed.GetComponent<RangerMovement>();
            var navMeshAgent = this.GetComponent<NavMeshAgent>();
            rangerMovement.Init(ranger.Movement, navMeshAgent, gridPositionMapping, gameSpeedManager);
        }


        private void OnTriggerEnter(Collider other) //move to rangerMovement later, also place the collider onto the ranger prefab
        {
            if (other.gameObject.CompareTag("Animal"))
            {
                UnityEngine.Debug.Log(other.gameObject.GetComponent<AnimalMovement>().behavior.Owner);
            }
           
        }

        void Start()
        {
        
        }

        void Update()
        {
        
        }
    }
}
