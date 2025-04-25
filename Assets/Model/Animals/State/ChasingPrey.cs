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
        private FollowPreyMovementCommand command;

        private IPrey prey;

        public ChasingPrey(Animal owner, float thirst, float hunger, FollowPreyMovementCommand command, IPrey prey) : base(owner, thirst, hunger)
        {
            this.command = command;
            this.prey = prey;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            command.Finished += OnChasingFinished;
            command.PreyEscaped += OnPreyEscaped;
            command.CanEscape = true;
            UnityEngine.Debug.Log($"{owner.GetType().Name} is chasing the prey");
        }

        private void OnPreyEscaped(object sender, EventArgs e)
        {
            command.Finished -= OnChasingFinished;
            command.ReportFinished();
            prey.OnEscaped();
            TransitionTo(new FailedHunting(this.owner, thirst, hunger));
        }

        private void OnChasingFinished(object sender, EventArgs e)
        {
            owner.Movement.AbortMovement();
            (sender as FollowPreyMovementCommand).Extra = null;
            prey.Kill();
            Debug.Log($"{owner.GetType().Name} killed {prey.GetType().Name}");
            TransitionTo(new PredatorEating(owner, thirst, hunger));
        }

        public override void OnExit()
        {
            base.OnExit();
            command.Finished -= OnChasingFinished;
            command.Cancel();
            prey.OnEscaped();

        }
    }
}
