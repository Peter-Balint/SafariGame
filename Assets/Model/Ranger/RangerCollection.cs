using Safari.Model.Animals;
using Safari.Model.GameSpeed;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Safari.Model.Rangers
{
    public class RangerCollection
    {
        private List<Ranger> rangers;
        public List<Ranger> Rangers { get { return rangers; } }

        public event EventHandler<Ranger>? Added;
        public event EventHandler<Ranger>? Removed;

        public RangerCollection()
        {
            rangers = new List<Ranger>();
        }

        public void Add(Ranger ranger)
        {
            rangers.Add(ranger);
            ranger.Died += OnRangerDied;
            Added?.Invoke(this, ranger);
        }

        public void Remove(Ranger ranger)
        {
            rangers.Remove(ranger);
            Removed?.Invoke(this, ranger);
        }

        private void OnRangerDied(object sender, EventArgs e)
        {
            if (sender is Ranger ranger)
            {
                Remove(ranger);
            }
        }
    }
}
