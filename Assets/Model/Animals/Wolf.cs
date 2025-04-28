#nullable enable
using Safari.Model.Movement;
using Safari.Model.Pathfinding;
using UnityEngine;


namespace Safari.Model.Animals
{
    public class Wolf : Predator
    {
        public Wolf(PathfindingHelper pathfinding, Group group, Vector3 wordPos) : base(pathfinding, group,wordPos)
        {
        }
        public Wolf(PathfindingHelper pathfinding, AnimalMetadata metadata, Group group, Vector3 wordPos) : base(pathfinding, metadata, group, wordPos)
        {
        }
    }
}
