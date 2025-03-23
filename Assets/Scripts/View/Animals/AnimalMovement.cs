using Safari.Model.Map;
using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.View.Animals
{
    public class AnimalMovement : MonoBehaviour
    {
        private Dictionary<GridPosition, Vector3> gridPositionMapping;
        public void Init(MovementBehavior behavior, Dictionary<GridPosition, Vector3> mapping)
        {
            gridPositionMapping = mapping;
        }
    }
}
