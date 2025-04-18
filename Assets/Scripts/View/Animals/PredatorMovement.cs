using Safari.Model.Animals.Movement;
using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Codice.Client.Common.WebApi.WebApiEndpoints;

namespace Safari.View.Animals
{
    public class PredatorMovement : AnimalMovement
    {
        private Transform? target;

        protected override void HandleMovement(MovementCommand command)
        {
            if (command is FollowPreyMovementCommand)
            {
                target = FindClosestPrey();
                return;
            }
            base.HandleMovement(command);
        }

        private void Update()
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
            }
        }

        private Transform FindClosestPrey()
        {

        }
    }
}
