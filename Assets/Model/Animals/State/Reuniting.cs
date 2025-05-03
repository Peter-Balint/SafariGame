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
        private Func<double, float ,State> nextState;

        public Reuniting(Animal owner, double hydrationPercent, float hunger, Func<double, float, State> nextState) : base(owner, hydrationPercent, hunger)
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
            TransitionTo(nextState(hydrationPercent, hunger));
        }
    }
}
