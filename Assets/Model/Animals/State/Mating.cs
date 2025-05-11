using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Animals.State
{
    public class Mating : State
    {
        public Mating(Animal owner, double hydrationPercent, double saturationPercent, double breedingCooldown) : base(owner, hydrationPercent, saturationPercent, breedingCooldown)
        {
        }

        public override bool CanMate()
        {
            return false;
        }
    }
}
