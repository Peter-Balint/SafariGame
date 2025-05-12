using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Animals.State
{
    public class Reuniting : State
    {
        private Func<double, double, double ,State> nextState;

        public Reuniting(Animal owner, double hydrationPercent, double saturationPercent, double breedingCooldown, Func<double, double, double,State> nextState) : base(owner, hydrationPercent, saturationPercent, breedingCooldown)
        {
            this.nextState = nextState;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            GridMovementCommand command = new GridMovementCommand(owner.Group.Leader.Movement.Location);
            command.Finished += OnCommandFinished;
            owner.Movement.ExecuteMovement(command);
        }

        private void OnCommandFinished(object sender, EventArgs e)
        {
            TransitionTo(nextState(hydrationPercent, saturationPercent, breedingCooldown));
        }
    }
}
