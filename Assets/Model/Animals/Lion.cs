#nullable enable
using UnityEngine;

namespace Safari.Model.Animals
{
    public class Lion : Predator
    {
        public Lion? Leader;

        public Lion(Lion? leader) : base()
        {
            Leader = leader;
        }
    }
}
