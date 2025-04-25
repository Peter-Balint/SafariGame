#nullable enable
using Safari.Model.Animals;
using Safari.Model.Animals.Movement;
using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

namespace Safari.View.Animals
{
    [RequireComponent(typeof(Rigidbody))]
    public class PredatorMovement : AnimalMovement
    {
        private Rigidbody rb;

        protected override void HandleMovement(MovementCommand command)
        {
            if (command is FollowPreyMovementCommand fc)
            {
                var target = FindClosestPrey();
                fc.Extra = target;
                if (target == null)
                {
                    fc.ReportPreyNotFound();
                }
                return;
            }
            base.HandleMovement(command);
        }

        protected override void CheckMovementFinished()
        {
            Vector3 getPosition() => rb.position;
            Vector3 getVelocity() => rb.linearVelocity;

            if (currentlyExecuting is not FollowPreyMovementCommand command)
            {
                base.CheckMovementFinished();
                return;
            }
            if (command.Extra is not Transform target)
            {
                return;
            }

            if (!agent.pathPending && agent.hasPath)
            {
                if (agent.remainingDistance < command.FinishedRadius)
                {
                    command.ReportFinished();
                }
                else if (agent.remainingDistance < command.StalkingFinishedRadius)
                {
                    Trace.Assert(target.parent.TryGetComponent<AnimalDisplay>(out var animalDisplay), "Chased prey is malformed: parent doesn't have AnimalDisplay");
                    Trace.Assert(animalDisplay.AnimalModel is IPrey, "Chased prey doesn't implement IPrey interface");

                    command.ReportPreyApproached(new PreyApproached(animalDisplay.AnimalModel as IPrey,
                                                                    new Chaser(getPosition, getVelocity)));
                }
                else if (command.CanEscape && agent.remainingDistance > command.EscapeRadius)
                {
                    command.ReportPreyEscaped();
                }
            }
        }

        protected override void Update()
        {
            base.Update();
            if (currentlyExecuting is FollowPreyMovementCommand && currentlyExecuting.Extra is Transform target)
            {
                agent.SetDestination(target.position);
            }
        }

        private Transform? FindClosestPrey()
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Prey");
            Transform? closest = null;
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

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
    }
}
