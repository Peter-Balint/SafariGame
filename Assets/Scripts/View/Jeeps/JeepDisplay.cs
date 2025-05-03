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
    public class JeepDisplay : MonoBehaviour
    {


        public Jeep? Jeep;

        public GameObject JeepPrefab;
		private GameObject? displayed;

		private Dictionary<GridPosition, Vector3> gridPositionMapping;
		public void Init(Jeep jeep, Dictionary<GridPosition, Vector3> gridPosMapping)
		{
			gridPositionMapping = gridPosMapping;
			Trace.Assert(displayed == null);
			DisplayJeep(jeep);
		}

		private void DisplayJeep(Jeep jeep)
        {
			this.Jeep = jeep;

			if (JeepPrefab == null) { return; }
			displayed = Instantiate(JeepPrefab, transform, false);

			
			
			

			
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
