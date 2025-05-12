using Safari.Model.Animals.Movement;
using System;
using UnityEngine;

namespace Safari.Model.Rangers
{
    public class Wandering : State
    {
        public Wandering(Ranger owner) : base(owner) { }
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
