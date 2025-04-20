using UnityEngine;
using Safari.Model.Rangers;
using Safari.Model.Map;
using System.Collections.Generic;
using System.Diagnostics;

namespace Safari.View.Rangers
{
    public class RangerDisplay : MonoBehaviour
    {
        public Ranger? Ranger;

        public GameObject RangerPrefab;
        private GameObject? displayed;

        private Dictionary<GridPosition, Vector3> gridPositionMapping;

        public void Init(Ranger ranger, Dictionary<GridPosition, Vector3> gridPosMapping)
        {
            gridPositionMapping = gridPosMapping;
            Trace.Assert(displayed == null);
            DisplayRanger(ranger);
        }

        private void DisplayRanger(Ranger ranger)
        {
            this.Ranger = ranger;

            if (RangerPrefab == null) { return; }
            displayed = Instantiate(RangerPrefab, transform, false);
        }

        void Start()
        {
        
        }

        void Update()
        {
        
        }
    }
}
