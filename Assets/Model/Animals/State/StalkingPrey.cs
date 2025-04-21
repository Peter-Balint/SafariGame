using Safari.Model.Animals.Movement;
using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Animals.State
{
    public class StalkingPrey : State
    {
        private const float HuntingRange = 120;

        private const float KillRange = 1.8f;

        private int counter = 0;

        public StalkingPrey(Animal owner, float thirst, float hunger) : base(owner, thirst, hunger)
        {
            counter = 0;
        }

        public StalkingPrey(Animal owner, float thirst, float hunger, int counter) : base(owner, thirst, hunger)
        {
            this.counter = counter;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            if (counter == 0)
            {
                var command = new FollowPreyMovementCommand(HuntingRange, KillRange);
                command.StalkingFinished += OnStalkingFinished;
                owner.Movement.ExecuteMovement(command);
                UnityEngine.Debug.Log($"{owner.GetType().Name} is searching for, and will stalk a prey");

            }
        }

        private void OnStalkingFinished(object sender, StalkingFinishedEventArgs e)
        {
            FollowPreyMovementCommand command = sender as FollowPreyMovementCommand;
            command.StalkingFinished -= OnStalkingFinished;
            switch (e.Result)
            {
                case PreyApproached a:
                    UnityEngine.Debug.Log($"{owner.GetType().Name} got close to the prey, hunting starts");
                    a.Prey.OnChased(a.Chaser);
                    TransitionTo(new ChasingPrey(
                        owner,
                        thirst,
                        hunger,
                        command,
                        a.Prey));
                    break;

                case PreyNotFound:
                    UnityEngine.Debug.Log($"{owner.GetType().Name} couldn't find a prey");
                    if (counter == 0)
                    {
                        TransitionTo(new StalkingPrey(owner, thirst, hunger, 40));
                    }
                    else
                    {
                        TransitionTo(new StalkingPrey(owner, thirst, hunger, counter - 1));
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
