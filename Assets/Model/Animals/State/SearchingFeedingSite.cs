using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.Model.Animals.State
{
    public class SearchingFeedingSite : State
    {
        public SearchingFeedingSite(Animal owner, float thirst, float hunger) : base(owner, thirst, hunger)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Searching for feeding site");
            GridMovementCommand? command = owner.Pathfinding.FindClosestFeedingSite(owner.Movement.Location);

            if (command is GridMovementCommand c)
            {
                c.Finished += OnFoundFeedingSite; ;
                Debug.Log($"{owner.GetType().Name} found a feeding site at {c.TargetCell.X} {c.TargetCell.Z}");

                owner.Movement.ExecuteMovement(c);
            }
            else
            {
                Debug.Log($"{owner.GetType().Name} couldn't find a feeding site");
            }
        }

        private void OnFoundFeedingSite(object sender, EventArgs e)
        {
            Debug.Log($"{owner.GetType().Name} reached the feeding site");
            TransitionTo(new Eating(owner, thirst, hunger));
        }
    }
}
