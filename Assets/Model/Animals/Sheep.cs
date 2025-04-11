#nullable enable
using Safari.Model.Pathfinding;
using UnityEngine;

namespace Safari.Model.Animals
{
    public class Sheep : Herbivore
    {
        public Sheep? Leader;

        public Sheep(PathfindingHelper pathfinding, Sheep? leader) : base(pathfinding)
        {
            Leader = leader;
        }
        public Sheep(PathfindingHelper pathfinding, AnimalMetadata metadata, Sheep? leader) : base(pathfinding, metadata)
        {
            Leader = leader;
        }
    }
}
