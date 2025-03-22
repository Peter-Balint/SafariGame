#nullable enable
using UnityEngine;

namespace Safari.Model.Animals
{
    public class Lion : Predator
    {
        public Lion? Leader;

        public Lion(Lion? leader)
        {
            Leader = leader;
            age = 0;
            hunger = 0;
            thirst = 0;
        }
    }
}
