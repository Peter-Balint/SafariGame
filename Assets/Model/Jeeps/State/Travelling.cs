using Safari.Model.Assets;
using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.Model.Jeeps.State
{
    public class Travelling : State
    {
        public int VisitorsOnBoard { get; private set; }

        private List<GridMovementCommand>? commands;

        public Travelling(Jeep owner, int visitorsOnBoard) : base(owner)
        {
            VisitorsOnBoard = visitorsOnBoard;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            commands = owner.Pathfinding.FindRandomPathToExit(owner.Movement.Location);
            if (commands == null)
            {
                Debug.Log("Jeep couldn't find Exit");
                TransitionToNextUpdate(new Travelling(owner, VisitorsOnBoard));
                return;
            }
            RunNextHop();

        }

        private void RunNextHop()
        {
            if (commands == null || commands.Count == 0)
            {
                return;
            }

            var command = commands.First();
            commands.RemoveAt(0);
            owner.Movement.ExecuteMovement(command);
            command.Finished += OnMovementFinished;
        }

        private void OnMovementFinished(object sender, EventArgs e)
        {
            (sender as GridMovementCommand).Finished -= OnMovementFinished;
            if (commands.Count == 0)
            {
                Debug.Log("Jeep reached exit");
                TransitionTo(new Returning(owner));
            }
            else
            {
                RunNextHop();
            }
        }
    }
}
