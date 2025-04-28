#nullable enable
using Safari.Model.Pathfinding;
using UnityEngine;

namespace Safari.Model.Animals
{
    public class Sheep : Herbivore
    {
        public Sheep(PathfindingHelper pathfinding, Group group, Vector3 wordPos) : base(pathfinding, group, wordPos)
        {
        }
        public Sheep(PathfindingHelper pathfinding, AnimalMetadata metadata, Group group, Vector3 wordPos) : base(pathfinding, metadata, group, wordPos)
        {
        }
    }
}
