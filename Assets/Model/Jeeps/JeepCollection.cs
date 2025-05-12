using Safari.Model.Assets;
using Safari.Model.Pathfinding;
using Safari.Model.Rangers;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Safari.Model.Jeeps
{
    public class JeepCollection
    {
        private List<Jeep> jeeps;
		public List<Jeep> Jeeps {  get { return jeeps; } }
        private readonly VisitorManager visitorManager;
        private readonly PathfindingHelper pathfindingHelper;

        public event EventHandler<Jeep>? Added;
		public event EventHandler<Jeep>? Removed;


		public JeepCollection(VisitorManager visitorManager, PathfindingHelper pathfindingHelper)
        {
            jeeps = new List<Jeep> ();
            this.visitorManager = visitorManager;
            this.pathfindingHelper = pathfindingHelper;
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

		public void CreateNewJeep(Vector3 pos)
		{
			var jeep = new Jeep(pos, visitorManager, pathfindingHelper);
            Add(jeep);
        }
	}
}
