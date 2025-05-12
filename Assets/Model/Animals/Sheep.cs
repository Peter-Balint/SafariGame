#nullable enable
using Safari.Model.Pathfinding;
using UnityEngine;

namespace Safari.Model.Animals
{
    public class Sheep : Herbivore
    {
        public Sheep(PathfindingHelper pathfinding, Group group, AnimalCollection collection, Vector3 wordPos) : base(pathfinding, group, collection, wordPos)
        {
        }
        public Sheep(PathfindingHelper pathfinding, AnimalMetadata metadata, Group group, AnimalCollection collection, Vector3 wordPos) : base(pathfinding, metadata, group, collection, wordPos)
        {
        }

        public override Animal OffspringFactory()
        {
            return new Sheep(Pathfinding, Metadata, Group, AnimalCollection, Movement.WordPos);
        }
    }
}
