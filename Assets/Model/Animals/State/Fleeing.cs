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

        public Fleeing(Animal owner, float thirst, float hunger, Chaser chaser) : base(owner, thirst, hunger)
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
