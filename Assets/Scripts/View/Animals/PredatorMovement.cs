using Safari.Model.Animals.Movement;
using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Safari.View.Animals
{
    public class PredatorMovement : AnimalMovement
    {
        private Transform? target;

        protected override void HandleMovement(MovementCommand command)
        {
            if (command is FollowPreyMovementCommand fc)
            {
                target = FindClosestPrey();
                if (target == null)
                {
                    fc.ReportPreyNotFound();
                }
                return;
            }
            base.HandleMovement(command);
        }

        protected override void OnCurrentMovementCancelled(object sender, EventArgs e)
        {
            if (sender is FollowPreyMovementCommand)
            {
                target = null;
            }
            base.OnCurrentMovementCancelled(sender, e);

        }

        protected override void Update()
        {
            if (target != null && currentlyExecuting is FollowPreyMovementCommand command)
            {
                agent.SetDestination(target.position);
                if (command.FinishDistance != null && !agent.pathPending && agent.remainingDistance < command.FinishDistance)
                {
                    command.ReportFinished();
                }
            }
            base.Update();
        }

        private Transform? FindClosestPrey()
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Prey");
            Transform closest = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = transform.position;

            foreach (GameObject target in targets)
            {
                float dist = Vector3.Distance(currentPos, target.transform.position);

                if (dist < minDist)
                {
                    minDist = dist;
                    closest = target.transform;
                }
            }

            return closest;
        }
    }
}
