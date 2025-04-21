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
using static PlasticPipe.PlasticProtocol.Messages.NegotiationCommand;

namespace Safari.View.Animals
{
    [RequireComponent(typeof(Rigidbody))]
    public class PredatorMovement : AnimalMovement
    {
        private Transform? target;

        private Rigidbody rb;

        private Transform? prevTarget;

        protected override void HandleMovement(MovementCommand command)
        {
            if (command is FollowPreyMovementCommand fc)
            {
                if (!fc.SearchPrey)
                {
                    target = target ?? prevTarget;
                    return;
                }
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
                if (target != null)
                {
                    prevTarget = target;
                }
                target = null;
            }
            base.OnCurrentMovementCancelled(sender, e);

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

            if (target == null)
            {
                return;
            }

            float stoppingDistance = command.FinishDistance ?? agent.stoppingDistance;

            if (!agent.pathPending
                && agent.hasPath
                && agent.remainingDistance < stoppingDistance)
            {
                Trace.Assert(target.parent.TryGetComponent<AnimalDisplay>(out var animalDisplay), "Chased prey is malformed: parent doesn't have AnimalDisplay");
                Trace.Assert(animalDisplay.AnimalModel is IPrey, "Chased prey doesn't implement IPrey interface");

                command.ReportPreyApproached(new PreyApproached(animalDisplay.AnimalModel as IPrey,
                                                                new Chaser(getPosition, getVelocity)));
            }
        }

        protected override void Update()
        {
            base.Update();
            if (target != null)
            {
                agent.SetDestination(target.position);
            }
        }

        protected override void OnMovementFinished(object sender, MovementFinishedEventArgs e)
        {
            base.OnMovementFinished(sender, e);
            if (target != null)
            {
                prevTarget = target;
            }
            target = null;
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

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
    }
}
