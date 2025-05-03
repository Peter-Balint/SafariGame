using Safari.Model;
using Safari.Model.GameSpeed;
using Safari.Model.Jeeps;
using Safari.Model.Map;
using Safari.Model.Rangers;
using Safari.View.Rangers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

namespace Safari.View.Jeeps
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(JeepMovement))]
    public class JeepDisplay : MonoBehaviour
    {
        public Jeep? Jeep;

		public void Init(Jeep jeep, Dictionary<GridPosition, Vector3> gridPosMapping)
		{
            Jeep = jeep;
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            GetComponent<JeepMovement>().Init(jeep.Movement, agent, gridPosMapping, SafariGame.Instance.GameSpeedManager);
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
