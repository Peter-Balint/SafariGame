#nullable enable
using Safari.Model.Animals.Movement;
using Safari.Model.GameSpeed;
using Safari.Model.Map;
using Safari.Model.Movement;
using Safari.View.Movement;
using Safari.View.Utils;
using Safari.View.World.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;
using UnityEngine.SocialPlatforms;

namespace Safari.View.Animals
{
    public class AnimalMovement : MovementBase
    {
        private MovementBehavior? target;

        protected override void HandleMovement(MovementCommand command)
        {
            switch (command)
            {
                case WanderingMovementCommand wandering:
                    HandleWandering(wandering);
                    break;

                case ApproachMovementCommand approach:
                    HandleApproach(approach);
                    break;
            }
            base.HandleMovement(command);
        }

        private void HandleWandering(WanderingMovementCommand command)
        {
            if (command.Followed != null && command.Followed.Extra is Vector3 target)
            {
                Debug.Log("Let's follow another wandering");
                agent.SetDestination(target);
                command.Extra = target;
                return;
            }

            if (NavMeshUtils.RandomPointOnNavMesh(out Vector3 point))
            {
                agent.SetDestination(point);
                command.Extra = point;
            }
            else
            {
                Debug.Log("Could not find valid position on NavMesh");
            }
        }

        private void HandleApproach(ApproachMovementCommand approach)
        {
            target = approach.Target;
        }

        protected override void OnCurrentMovementCancelled(object sender, EventArgs e)
        {
            if (currentlyExecuting is ApproachMovementCommand a && a.Target == target)
            {
                target = null;
            }
            base.OnCurrentMovementCancelled(sender, e);
        }

        protected override void OnMovementFinished(object sender, EventArgs e)
        {
            if (currentlyExecuting is ApproachMovementCommand a && a.Target == target)
            {
                target = null;
            }
            base.OnMovementFinished(sender, e);
        }

        protected override void Update()
        {
            if (target != null)
            {
                agent.SetDestination(target.WordPos);
            }
            base.Update();
        }
    }
}
