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
        private Func<float, float ,State> nextState;

        public Reuniting(Animal owner, float thirst, float hunger, Func<float, float, State> nextState) : base(owner, thirst, hunger)
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
            TransitionTo(nextState(thirst, hunger));
        }
    }
}
