#nullable enable
using Safari.Model.Animals;
using Safari.Model.Animals.Movement;
using Safari.Model.Animals.State;
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
                if (target == null)
                {
                    fc.Extra = null;
                    fc.ReportPreyNotFound();
                    return;
                }
                AnimalDisplay? animalDisplay = null;
                Trace.Assert(target.parent.TryGetComponent<AnimalDisplay>(out animalDisplay), "Target prey is malformed: parent doesn't have AnimalDisplay");
                Trace.Assert(animalDisplay.AnimalModel is IPrey, "Target prey doesn't implement IPrey interface");
                if (animalDisplay != null && animalDisplay.AnimalModel != null)
                {
                    fc.Extra = new Tuple<Transform, Animal>(target, animalDisplay.AnimalModel);
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
            if (command.Extra is not Tuple<Transform, Animal> extra)
            {
                return;
            }
            var target = extra.Item1;
            var animal = extra.Item2;

            if (!agent.pathPending && agent.hasPath)
            {
                if (agent.remainingDistance < command.FinishedRadius)
                {
                    command.ReportFinished();
                }
                else if (agent.remainingDistance < command.StalkingFinishedRadius && !command.IsStalkingFinished)
                {
                    command.ReportPreyApproached(new PreyApproached(animal as IPrey,
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
            if (currentlyExecuting is FollowPreyMovementCommand && currentlyExecuting.Extra is Tuple<Transform, Animal> extra && extra.Item1 != null)
            {
                agent.SetDestination(extra.Item1.position);
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
            base.Start();
            rb = GetComponent<Rigidbody>();
        }
    }
}
