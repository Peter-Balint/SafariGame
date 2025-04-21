#nullable enable
using Safari.Model.Animals;
using Safari.Model.Animals.Movement;
using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

namespace Safari.View.Animals
{
    public class PreyMovement : AnimalMovement
    {
        public float escapeTriggerDistance = 3f;
        public float fleeDistance = 60f;
        public float predictionTime = 1.5f;
        public int maxAttempts = 60;

        private Chaser? chaser;
        private Vector3 currentEscapeTarget = Vector3.zero;
        private bool firstCalc = false;

        protected override void HandleMovement(MovementCommand command)
        {
            if (command is FleeMovementCommand fc)
            {
                Debug.Log("FleeMovementCommand");
                chaser = fc.Chaser;
                firstCalc = true;
                return;
            }
            base.HandleMovement(command);
        }

        protected override void Update()
        {
            base.Update();
            if (chaser == null)
            {
                return;
            }

            float distToThreat = Vector3.Distance(transform.position, chaser.GetPosition());
            float distToTarget = Vector3.Distance(transform.position, currentEscapeTarget);
            
            bool shouldRecalculate = firstCalc || distToThreat < escapeTriggerDistance || distToTarget < 8f;

            if (shouldRecalculate)
            {
                firstCalc = false;
                Vector3 bestEscapePoint = FindSmartEscapePoint(chaser);
                if (bestEscapePoint != Vector3.zero)
                {
                    currentEscapeTarget = bestEscapePoint;
                    agent.SetDestination(bestEscapePoint);
                }
            }

        }

        protected override void CheckMovementFinished()
        {
           
        }

        private Vector3 FindSmartEscapePoint(Chaser chaser)
        {
            Vector3 bestPoint = Vector3.zero;
            float bestScore = float.MinValue;

            for (int i = 0; i < maxAttempts; i++)
            {
                float angle = Random.Range(0f, 360f);
                float distance = Random.Range(fleeDistance * 0.5f, fleeDistance * 1.5f);

                Vector3 randomDir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
                Vector3 candidate = transform.position + randomDir * distance;

                if (NavMesh.SamplePosition(candidate, out NavMeshHit hit, 2f, NavMesh.AllAreas))
                {
                    Debug.DrawRay(hit.position, Vector3.up * 60, Color.red, 3f);
                    NavMeshPath path = new NavMeshPath();
                    if (NavMesh.CalculatePath(hit.position, chaser.GetPosition(), NavMesh.AllAreas, path))
                    {
                        float totalDistance = GetPathLength(path);
                        if (totalDistance > bestScore)
                        {
                            bestScore = totalDistance;
                            bestPoint = hit.position;
                        }
                    }
                }
            }
            Debug.DrawRay(bestPoint, Vector3.up * 60, Color.blue, 1f);

            return bestPoint;
        }

        private float GetPathLength(NavMeshPath path)
        {
            float length = 0f;
            for (int i = 1; i < path.corners.Length; i++)
            {
                length += Vector3.Distance(path.corners[i - 1], path.corners[i]);
            }
            return length;
        }
    }
}
