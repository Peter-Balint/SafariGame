#nullable enable
using Safari.Model.Movement;
using Safari.Model.Pathfinding;
using UnityEngine;


namespace Safari.Model.Animals
{
    public class Wolf : Predator
    {
        public Wolf(PathfindingHelper pathfinding, Group group, AnimalCollection collection, Vector3 wordPos) : base(pathfinding, group, collection, wordPos)
        {
        }
        public Wolf(PathfindingHelper pathfinding, AnimalMetadata metadata, Group group, AnimalCollection collection, Vector3 wordPos) : base(pathfinding, metadata, group, collection, wordPos)
        {
        }

        public override Animal OffspringFactory()
        {
            return new Wolf(Pathfinding, Metadata, Group, AnimalCollection, Movement.WordPos);
        }
    }
}
