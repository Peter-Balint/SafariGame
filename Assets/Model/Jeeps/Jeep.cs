using Safari.Model.Map;
using Safari.Model.Movement;
using System.Collections.Generic;
using UnityEngine;

namespace Safari.Model.Jeeps
{
    public class Jeep : IMoving
    {
        public Vector3 Position { get; set; }
        public MovementBehavior Movement;

		public Jeep(Vector3 vec3)
		{
			Position = vec3;
			Movement = new MovementBehavior(this, Position);
		}
	}
}
