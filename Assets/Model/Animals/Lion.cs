#nullable enable
using Safari.Model.Pathfinding;
using UnityEngine;

namespace Safari.Model.Animals
{
    public class Lion : Predator
    {
        public Lion(PathfindingHelper pathfinding, Group group, AnimalCollection collection, Vector3 wordPos) : base(pathfinding, group, collection, wordPos)
        {
        }
        public Lion(PathfindingHelper pathfinding, AnimalMetadata metadata, Group group, AnimalCollection collection, Vector3 wordPos) : base(pathfinding, metadata, group, collection, wordPos)
        {
        }

        public override Animal OffspringFactory()
        {
            return new Lion(Pathfinding, Metadata, Group, AnimalCollection, Movement.WordPos);
        }
    }
}
