#nullable enable
using Safari.Model.Movement;
using UnityEngine;


namespace Safari.Model.Animals
{
    public class Wolf : Predator
    {
        public Wolf? Leader;

        public Wolf(Wolf? leader) : base()
        {
            Leader = leader;
            age = 0;
            hunger = 0;
            thirst = 0;
        }
    }
}
