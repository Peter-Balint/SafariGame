using System;
using UnityEngine;

namespace Safari.Model.Rangers
{
    public class Ranger
    {
        public Vector3 Position {  get; set; }

        public event EventHandler? Died;

        public Ranger() 
        {
            Position = Vector3.zero;
        }
    }
}
