#nullable enable
using System.Collections.Generic;
using System;
using UnityEngine;
using Safari.Model.GameSpeed;

namespace Safari.Model.Hunters
{
    public class HunterCollection
    {
        private List<Hunter> hunters;
        public List<Hunter> Hunters { get { return hunters; } }

        public event EventHandler<Hunter>? Added;
        public event EventHandler<Hunter>? Removed;


        public HunterCollection() 
        { 
            hunters = new List<Hunter>();
        }

        public void Add(Hunter hunter)
        {
            hunters.Add(hunter);
            hunter.Died += OnHunterDied;
            Added?.Invoke(this, hunter);
        }

        public void Remove(Hunter hunter)
        {
            hunters.Remove(hunter);
            Removed?.Invoke(this, hunter);
        }

        private void OnHunterDied(object sender, EventArgs e)
        {
            if (sender is Hunter hunter)
            {
                Remove(hunter);
            }
        }
    }
}
