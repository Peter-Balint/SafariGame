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
using System;

namespace Safari.View.Rangers
{
    public class RangerDisplay : MonoBehaviour
    {
        public Ranger? Ranger;

        public GameObject RangerPrefab;
        private GameObject? displayed;

        private GameSpeedManager gameSpeedManager;

        private Dictionary<GridPosition, Vector3> gridPositionMapping;

        public event EventHandler OnCLick;

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

            rangerMovement.OnClick += ((sender, args) => OnCLick?.Invoke(this, args));
        }



        void Start()
        {
        
        }

        void Update()
        {
        
        }
    }
}
