#nullable enable
using Safari.Model.Pathfinding;
using UnityEngine;

namespace Safari.Model.Animals
{
    public class Lion : Predator
    {
        public Lion(PathfindingHelper pathfinding, Group group, Vector3 wordPos) : base(pathfinding, group, wordPos)
        {
        }
        public Lion(PathfindingHelper pathfinding, AnimalMetadata metadata, Group group, Vector3 wordPos) : base(pathfinding, metadata, group, wordPos)
        {
        }
    }
}
