using Safari.Model.Animals.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.Model.Animals.State
{
    public class Wandering : Animals.State.State
    {
        public Wandering(Animal owner, float thirst) : base(owner, thirst)
        {
        }

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
            TransitionTo(new Wandering(owner, thirst));
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            AllowSearchingWater();
        }
    }
}
