using UnityEngine;

namespace Safari.Model.Map
{
    public class Entrance: Field
    {
		public override bool CanDemolish => false;

		public Entrance() : base(BuildingMetadata.Default())
		{
		}

		public override bool CanPlaceHere(Field field)
		{
			return false;
		}
	}
}
