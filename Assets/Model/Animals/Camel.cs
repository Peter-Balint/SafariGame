#nullable enable
using Safari.Model.Pathfinding;
using UnityEngine;

namespace Safari.Model.Animals
{
    public class Camel : Herbivore
    {
        public Camel? Leader;

        public Camel(PathfindingHelper pathfinding, Camel? leader) : base(pathfinding)
        {
            Leader = leader;
        }
        public Camel(PathfindingHelper pathfinding, AnimalMetadata metadata, Camel? leader) : base(pathfinding, metadata)
        {
            Leader = leader;
        }
    }
}
