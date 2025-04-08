#nullable enable
using Safari.Model.Animals.Movement;
using Safari.Model.Map;
using Safari.Model.Movement;
using Safari.View.World.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Safari.View.Animals
{
    public class AnimalMovement : MonoBehaviour
    {
        public float cellSize = 30f;

        private MovementBehavior behavior;
        private NavMeshAgent agent;
        private Dictionary<GridPosition, Vector3> gridPositionMapping;

        private MovementCommand? currentlyExecuting;

        public void Init(MovementBehavior behavior, NavMeshAgent agent, Dictionary<GridPosition, Vector3> mapping)
        {
            this.behavior = behavior;
            this.agent = agent;

            behavior.CommandStarted += OnCommandStarted;
            gridPositionMapping = mapping;
            if (behavior.CurrentCommand != null && behavior.CurrentCommand != currentlyExecuting)
            {
                MoveAgent(behavior.CurrentCommand);
            }
        }

        private void OnCommandStarted(object sender, MovementCommand e)
        {
            MoveAgent(e);
        }

        private void MoveAgent(MovementCommand movementCommand)
        {
            movementCommand.Cancelled += OnMovementCancelled;
            currentlyExecuting = movementCommand;
            agent.ResetPath();
            switch (movementCommand)
            {
                case GridMovementCommand gm:
                    var target = gridPositionMapping[gm.TargetCell];
                    target += new Vector3(gm.TargetOffset.DeltaX, 0, gm.TargetOffset.DeltaZ) * 15;
                    agent.SetDestination(target);
                    break;

                case WanderingMovementCommand wanderingMovementCommand:
                    Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * 10000;
                    randomDirection += transform.position;

                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(randomDirection, out hit, 10000, NavMesh.AllAreas))
                    {
                        Debug.Log($"Wandering to {hit.position}");

                        agent.SetDestination(new Vector3(hit.position.x, transform.position.y, hit.position.z));
                    }
                    else
                    {
                        Debug.Log("Could not find valid position on NavMesh");
                    }
                    break;

            }
        }

        private void OnMovementCancelled(object sender, EventArgs e)
        {
            if (sender == currentlyExecuting)
            {
                agent.ResetPath();
            }
        }

        private void OnDestroy()
        {
            if (behavior == null)
            {
                return;
            }
            behavior.CommandStarted -= OnCommandStarted;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                var fieldDisplay = other.gameObject.GetComponent<FieldDisplay>();
                behavior.ReportLocation(fieldDisplay.Position);
            }
        }

        private void Update()
        {
            if (currentlyExecuting != null && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f))
            {
                agent.ResetPath();
                currentlyExecuting.ReportFinished();
            }
        }
    }
}
