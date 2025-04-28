
using UnityEngine;

namespace Safari.Model.Map
{
    public class Exit: Field
    {
		public override bool CanDemolish => false;

		public Exit() : base(BuildingMetadata.Default())
		{
		}

		public override bool CanPlaceHere(Field field)
		{
			return false;
		}
	}
}
