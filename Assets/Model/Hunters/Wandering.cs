using System;
using UnityEngine;
using Safari.Model.Animals.Movement;

namespace Safari.Model.Hunters
{
    public class Wandering : State
    {
        public Wandering(Hunter owner) : base(owner) { }
        public override void OnEnter()
        {
            Debug.Log("Wandering started");
            base.OnEnter();
            WanderingMovementCommand command = new WanderingMovementCommand();
            command.Finished += OnWanderingFinished;
            owner.Movement.ExecuteMovement(command);
        }

        private void OnWanderingFinished(object sender, EventArgs e)
        {
            Debug.Log("Wandering finished");
            TransitionTo(new Wandering(owner));
        }
    }
}
