using Safari.Model.Rangers;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Safari.Model.Jeep
{
    public class JeepCollection
    {
        private List<Jeep> jeeps;

		public event EventHandler<Jeep>? Added;
		public event EventHandler<Jeep>? Removed;


		public JeepCollection()
        {
            jeeps = new List<Jeep> ();

        }

		public void Add(Jeep jeep)
		{
			jeeps.Add(jeep);
			
			Added?.Invoke(this, jeep);
		}

		public void Remove(Jeep jeep)
		{
			jeeps.Remove(jeep);
			Removed?.Invoke(this, jeep);
		}

	}
}
