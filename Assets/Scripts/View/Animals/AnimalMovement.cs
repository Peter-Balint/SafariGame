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
        private MovementBehavior behavior;
        private NavMeshAgent agent;
        private Dictionary<GridPosition, Vector3> gridPositionMapping;
        public void Init(MovementBehavior behavior, NavMeshAgent agent, Dictionary<GridPosition, Vector3> mapping)
        {
            this.behavior = behavior;
            this.agent = agent;

            behavior.CommandStarted += MoveAgent;
            gridPositionMapping = mapping;
        }
        

        private void MoveAgent(object sender, MovementCommand movementCommand)
        {
            //movementCommand.Cancelled += StopAgent;
            var target = gridPositionMapping[movementCommand.TargetCell];
            agent.SetDestination(target);
        }

        private void OnDestroy()
        {
            if (behavior == null)
            {
                return;
            }
            behavior.CommandStarted -= MoveAgent;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                var fieldDisplay = other.gameObject.GetComponent<FieldDisplay>();
                behavior.ReportLocation(fieldDisplay.Position);
            }
        }

        /*private void StopAgent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }*/
    }
}
