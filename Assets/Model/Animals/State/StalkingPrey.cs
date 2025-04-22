#nullable enable
using Safari.Model.Animals.Movement;
using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PlasticPipe.PlasticProtocol.Messages.NegotiationCommand;

namespace Safari.Model.Animals.State
{
    public class StalkingPrey : State
    {
        private const float HuntingRange = 120;

        private const float KillRange = 1.8f;

        private FollowPreyMovementCommand? command;

        private IPrey? prey;

        public StalkingPrey(Animal owner, float thirst, float hunger) : base(owner, thirst, hunger)
        {
        }


        public override void OnEnter()
        {
            base.OnEnter();
            UnityEngine.Debug.Log($"{owner.GetType().Name} is searching for, and will stalk a prey");
            command = new FollowPreyMovementCommand(HuntingRange, KillRange);
            command.StalkingFinished += OnStalkingFinished;
            owner.Movement.ExecuteMovement(command);

        }

        private void OnStalkingFinished(object sender, StalkingFinishedEventArgs e)
        {
            FollowPreyMovementCommand command = (FollowPreyMovementCommand)sender;
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
                    // IMPORTANT: if prey not found, this event gets raised right after the ExecuteMovement call
                    // Trying to search for a prey again re-triggers this command, sequentially, leaving no chance
                    // for Unity to get the control back
                    // Also throws a stack overflow because re-searching for a prey is basically an infinite cycle
                    // So we wait till next update 
                    TransitionToNextUpdate(new StalkingPrey(owner, thirst, hunger));
                    break;

                default:
                    break;
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            if (command != null)
            {
                command.StalkingFinished -= OnStalkingFinished;
                command.Cancel();
            }
            prey?.OnEscaped();

        }
    }
}
