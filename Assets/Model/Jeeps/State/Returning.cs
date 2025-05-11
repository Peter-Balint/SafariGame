using Safari.Model.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.Model.Jeeps.State
{
    public class Returning : State
    {
        public Returning(Jeep owner) : base(owner)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Jeep is returning to the start");
            var command = owner.Pathfinding.FindEntrance(owner.Movement.Location);
            command.Finished += OnMovementFinished;
            owner.Movement.ExecuteMovement(command);
        }

        private void OnMovementFinished(object sender, EventArgs e)
        {
            TransitionTo(new Idling(owner));
        }
    }
}
