using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Animals.State
{
    public class SearchingMate : State
    {
        public SearchingMate(Animal owner, double hydrationPercent, double saturationPercent, double breedingCooldown) : base(owner, hydrationPercent, saturationPercent, breedingCooldown)
        {
        }
    }
}
