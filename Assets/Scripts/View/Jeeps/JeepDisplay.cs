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

		/// <summary>
		/// Initializes the Jeep display with the given Jeep data and sets up its movement using the NavMesh agent and grid position mapping.
		/// </summary>
		/// <param name="jeep">The Jeep data to associate with this display.</param>
		/// <param name="gridPosMapping">A mapping from grid positions to world positions for movement calculations.</param>
		public void Init(Jeep jeep, Dictionary<GridPosition, Vector3> gridPosMapping)
		{
            Jeep = jeep;
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            GetComponent<JeepMovement>().Init(jeep.Movement, agent, gridPosMapping, SafariGame.Instance.GameSpeedManager);
        }


		/// <summary>
		/// Updates the Jeep's model logic every frame, scaled by the current game speed.
		/// </summary>
		void Update()
        {
            Jeep?.ModelUpdate(Time.deltaTime, SafariGame.Instance.GameSpeedManager.CurrentSpeedToNum());
        }



    }
}
