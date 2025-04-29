#nullable enable
using Safari.Model.Pathfinding;
using UnityEngine;

namespace Safari.Model.Animals
{
    public class Camel : Herbivore
    {
        public Camel(PathfindingHelper pathfinding, Group group, Vector3 wordPos) : base(pathfinding, group, wordPos)
        {
        }
        public Camel(PathfindingHelper pathfinding, AnimalMetadata metadata, Group group, Vector3 wordPos) : base(pathfinding, metadata, group, wordPos)
        {
        }
    }
}
