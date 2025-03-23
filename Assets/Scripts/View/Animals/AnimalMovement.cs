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
    public class AnimalMovement : MonoBehaviour
    {
        private MovementBehavior behavior;
        private NavMeshAgent agent;
        public void Init(MovementBehavior behavior, NavMeshAgent agent)
        {
            this.behavior = behavior;
            this.agent = agent;

            behavior.CommandStarted += MoveAgent;
        }

        private void MoveAgent(object sender, MovementCommand movementCommand)
        {
            //movementCommand.Cancelled += StopAgent;
            //agent.SetDestination()
        }

        /*private void StopAgent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }*/
    }
}
