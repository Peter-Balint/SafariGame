
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Safari.Model.Map;
using Safari.Model.Movement;
using UnityEngine;


namespace EditorTests.Movement
{
    public class MovementBehaviorTests
    {
        private class MockMoving : IMoving { }

        private class MockMovementCommand : MovementCommand
        {
            public bool CancelCalled { get; private set; }
            public event EventHandler? Cancelled;
            public event EventHandler? Finished;

            public override void Cancel()
            {
                CancelCalled = true;
                Cancelled?.Invoke(this, EventArgs.Empty);
            }

            public override void ReportFinished()
            {
                Finished?.Invoke(this, EventArgs.Empty);
            }
        }

        [Test]
        public void ExecuteMovement_CancelsPreviousCommandAndSubscribesToNewEvents()
        {
            var owner = new MockMoving();
            var behavior = new MovementBehavior(owner, Vector3.zero);

            var cmd1 = new MockMovementCommand();
            var cmd2 = new MockMovementCommand();

            behavior.ExecuteMovement(cmd1);
            Assert.AreEqual(cmd1, behavior.CurrentCommand);

            bool commandStartedFired = false;
            behavior.CommandStarted += (s, c) => { commandStartedFired = true; };

            behavior.ExecuteMovement(cmd2);

            Assert.IsTrue(cmd1.CancelCalled, "Previous command should be cancelled.");
            Assert.AreEqual(cmd2, behavior.CurrentCommand, "CurrentCommand should be updated.");
            Assert.IsTrue(commandStartedFired, "CommandStarted event should fire.");
        }

        [Test]
        public void AbortMovement_CancelsCurrentCommandAndClearsState()
        {
            var owner = new MockMoving();
            var behavior = new MovementBehavior(owner, Vector3.zero);

            var cmd = new MockMovementCommand();
            behavior.ExecuteMovement(cmd);

            behavior.AbortMovement();

            Assert.IsTrue(cmd.CancelCalled, "AbortMovement should cancel the command.");
            Assert.IsNull(behavior.CurrentCommand, "CurrentCommand should be null after abort.");
        }

        [Test]
        public void OnCommandCancelled_OnlyClearsCurrentCommandIfSenderMatches()
        {
            var owner = new MockMoving();
            var behavior = new MovementBehavior(owner, Vector3.zero);

            var cmd1 = new MockMovementCommand();
            var cmd2 = new MockMovementCommand();

            behavior.ExecuteMovement(cmd1);

            // Simulate cancellation from a different command
            var onCommandCancelled = typeof(MovementBehavior)
                .GetMethod("OnCommandCancelled", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            onCommandCancelled.Invoke(behavior, new object[] { cmd2, EventArgs.Empty });

            Assert.AreEqual(cmd1, behavior.CurrentCommand, "CurrentCommand should not be cleared if sender does not match.");
        }

        [Test]
        public void ReportLocation_UpdatesLocationAndRaisesEvent()
        {
            var owner = new MockMoving();
            var behavior = new MovementBehavior(owner, Vector3.zero);

            GridPosition? reported = null;
            behavior.GridPositionChanged += (s, pos) => reported = pos;

            var newPos = new GridPosition(3, 4);
            behavior.ReportLocation(newPos);

            Assert.AreEqual(newPos, behavior.Location, "Location should be updated.");
            Assert.AreEqual(newPos, reported, "GridPositionChanged should be raised with new location.");
        }

        [Test]
        public void ReportWordPos_UpdatesWordPos()
        {
            var owner = new MockMoving();
            var behavior = new MovementBehavior(owner, Vector3.zero);

            var newPos = new Vector3(5, 0, 5);
            behavior.ReportWordPos(newPos);

            Assert.AreEqual(newPos, behavior.WordPos, "WordPos should be updated.");
        }

        [Test]
        public void CommandStarted_EventFiresWithCorrectCommand()
        {
            var owner = new MockMoving();
            var behavior = new MovementBehavior(owner, Vector3.zero);

            MovementCommand? startedCmd = null;
            behavior.CommandStarted += (s, cmd) => startedCmd = cmd;

            var cmd = new MockMovementCommand();
            behavior.ExecuteMovement(cmd);

            Assert.AreEqual(cmd, startedCmd, "CommandStarted should fire with the correct command.");
        }

        [Test]
        public void OnCommandFinished_OnlyClearsCurrentCommandIfSenderMatches()
        {
            var owner = new MockMoving();
            var behavior = new MovementBehavior(owner, Vector3.zero);

            var cmd1 = new MockMovementCommand();
            var cmd2 = new MockMovementCommand();

            behavior.ExecuteMovement(cmd1);

            // Simulate finished from a different command
            var onCommandFinished = typeof(MovementBehavior)
                .GetMethod("OnCommandFinished", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            onCommandFinished.Invoke(behavior, new object[] { cmd2, EventArgs.Empty });

            Assert.AreEqual(cmd1, behavior.CurrentCommand, "CurrentCommand should not be cleared if sender does not match.");
        }

        [Test]
        public void MultipleSequentialMovements_CancelsPreviousAndUpdatesCurrent()
        {
            var owner = new MockMoving();
            var behavior = new MovementBehavior(owner, Vector3.zero);

            var cmd1 = new MockMovementCommand();
            var cmd2 = new MockMovementCommand();
            var cmd3 = new MockMovementCommand();

            behavior.ExecuteMovement(cmd1);
            behavior.ExecuteMovement(cmd2);
            behavior.ExecuteMovement(cmd3);

            Assert.IsTrue(cmd1.CancelCalled, "First command should be cancelled.");
            Assert.IsTrue(cmd2.CancelCalled, "Second command should be cancelled.");
            Assert.AreEqual(cmd3, behavior.CurrentCommand, "CurrentCommand should be the last command.");
        }
    }
}
