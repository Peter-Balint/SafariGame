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
        private const float HuntingRange = 30;
        
        public StalkingPrey(Animal owner, float thirst, float hunger) : base(owner, thirst, hunger)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            var command = new FollowPreyMovementCommand(HuntingRange);
            command.Finished += OnStalkingFinished;
            owner.Movement.ExecuteMovement(command);
            UnityEngine.Debug.Log($"{owner.GetType().Name} is stalking the prey");
        }

        private void OnStalkingFinished(object sender, MovementFinishedEventArgs e)
        {
            UnityEngine.Debug.Log($"{owner.GetType().Name} got close to the prey, hunting starts");

            switch (e.Result)
            {
                case Success:
                    //TransitionTo(new StalkingPrey(owner, thirst, hunger));
                    break;

                case PreyNotFound:
                    TransitionTo(new StalkingPrey(owner, thirst, hunger));
                    break;

                default:
                    break;
            }
        }
    }
}
