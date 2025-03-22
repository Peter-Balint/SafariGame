#nullable enable
using UnityEngine;

namespace Safari.Model.Animals
{
    public class Camel : Herbivore
    {
        public Camel? Leader;

        public Camel(Camel? leader)
        {
            Leader = leader;
            age = 0;
            hunger = 0;
            thirst = 0;
        }
    }
}
