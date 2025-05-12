#nullable enable
using Safari.Model.Pathfinding;
using UnityEngine;

namespace Safari.Model.Animals
{
    public class Camel : Herbivore
    {
        public Camel(PathfindingHelper pathfinding, Group group, AnimalCollection collection, Vector3 wordPos) : base(pathfinding, group, collection, wordPos)
        {
        }
        public Camel(PathfindingHelper pathfinding, AnimalMetadata metadata, Group group, AnimalCollection collection, Vector3 wordPos) : base(pathfinding, metadata, group, collection, wordPos)
        {
        }

        public override Animal OffspringFactory()
        {
            return new Camel(Pathfinding, Metadata, Group, AnimalCollection, Movement.WordPos);
        }
    }
}
