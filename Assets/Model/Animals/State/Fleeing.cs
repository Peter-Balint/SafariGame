using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Animals.State
{
    public class Fleeing : State
    {
        private Chaser chaser;

        public Fleeing(Animal owner, double hydrationPercent, double saturationPercent, double breedingCooldown, Chaser chaser) : base(owner, hydrationPercent, saturationPercent, breedingCooldown)
        {
            this.chaser = chaser;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            owner.Movement.ExecuteMovement(new Movement.FleeMovementCommand(chaser));
        }
    }
}
