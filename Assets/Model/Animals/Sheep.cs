#nullable enable
using UnityEngine;

namespace Safari.Model.Animals
{
    public class Sheep : Herbivore
    {
        public Sheep? Leader;

        public Sheep(Sheep? leader)
        {
            Leader = leader;
            age = 0;
            hunger = 0;
            thirst = 0;
        }
    }
}
