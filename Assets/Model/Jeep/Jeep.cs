using Safari.Model.Map;
using Safari.Model.Movement;
using System.Collections.Generic;
using UnityEngine;

namespace Safari.Model.Jeep
{
    public class Jeep : IMoving
    {
		public Vector3 Position { get; set; }
		public MovementBehavior Movement;



		public Jeep()
		{
			Movement = new MovementBehavior(this);
			Position = new Vector3(8,0,137); // it should be enterance tile

		}

		public Jeep(Vector3 vec3)
		{
			Movement = new MovementBehavior(this);
			Position = vec3;
		}





	}
}
