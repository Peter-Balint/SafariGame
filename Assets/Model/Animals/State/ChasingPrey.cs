using Safari.Model.Animals.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.Model.Animals.State
{
    public class ChasingPrey : State
    {
        public const float KillRange = 1.8f;

        public ChasingPrey(Animal owner, float thirst, float hunger) : base(owner, thirst, hunger)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            var command = FollowPreyMovementCommand.Chase(KillRange);
            command.Finished += OnChasingFinished; 
            owner.Movement.ExecuteMovement(command);
            UnityEngine.Debug.Log($"{owner.GetType().Name} is chasing the prey");
        }

        private void OnChasingFinished(object sender, Model.Movement.MovementFinishedEventArgs e)
        {
            switch (e.Result)
            {
                case PreyApproached a:
                    owner.Movement.AbortMovement();
                    a.Prey.Kill();
                    Debug.Log($"{owner.GetType().Name} killed {a.Prey.GetType().Name}");
                    TransitionTo(new PredatorEating(owner, thirst, hunger));
                    break;

                default:
                    break;
            }
        }
    }
}
