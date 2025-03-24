#nullable enable
using Safari.Model.Movement;
using Safari.Model.Pathfinding;
using UnityEngine;


namespace Safari.Model.Animals
{
    public class Wolf : Predator
    {
        public Wolf? Leader;

        public Wolf(PathfindingHelper pathfinding,Wolf? leader) : base(pathfinding)
        {
            Leader = leader;
        }
    }
}
