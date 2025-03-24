#nullable enable
using Safari.Model.Pathfinding;
using UnityEngine;

namespace Safari.Model.Animals
{
    public class Lion : Predator
    {
        public Lion? Leader;

        public Lion(PathfindingHelper pathfinding, Lion? leader) : base(pathfinding)
        {
            Leader = leader;
        }
    }
}
