#nullable enable
using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.Model.Animals.State
{
    public class SearchingWater : State
    {
        public SearchingWater(Animal owner, float thirst, float hunger) : base(owner, thirst, hunger)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Searching for water");
            GridMovementCommand? command = owner.Pathfinding.FindClosestDrinkingPlace(owner.Movement.Location);

            if (command is GridMovementCommand c)
            {
                c.Finished += OnFoundWater;
                Debug.Log($"{owner.GetType().Name} found a drinking place at {c.TargetCell.X} {c.TargetCell.Z}");

                owner.Movement.ExecuteMovement(c);
            }
            else
            {
                Debug.Log($"{owner.GetType().Name} couldn't find a drinking place");
            }
        }

        private void OnFoundWater(object sender, EventArgs e)
        {
            Debug.Log($"{owner.GetType().Name} reached the drinking place");
            TransitionTo(new Wandering(owner, thirst, hunger));
        }
    }
}
