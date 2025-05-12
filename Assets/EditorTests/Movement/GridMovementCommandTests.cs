using NUnit.Framework;
using Safari.Model.Map;
using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorTests.Movement
{
    public class GridMovementCommandTests
    {
        [Test]
        public void Constructor_WithGridPosition_SetsTargetOffsetToZero()
        {
            var pos = new GridPosition(1, 2);
            var cmd = new GridMovementCommand(pos);

            Assert.AreEqual(pos, cmd.TargetCell);
            Assert.AreEqual(GridMovementCommand.Offset.Zero.DeltaX, cmd.TargetOffset.DeltaX);
            Assert.AreEqual(GridMovementCommand.Offset.Zero.DeltaZ, cmd.TargetOffset.DeltaZ);
        }

        [Test]
        public void Constructor_WithGridPositionAndOffset_SetsProperties()
        {
            var pos = new GridPosition(3, 4);
            var offset = new GridMovementCommand.Offset(1.5f, -2.5f);
            var cmd = new GridMovementCommand(pos, offset);

            Assert.AreEqual(pos, cmd.TargetCell);
            Assert.AreEqual(1.5f, cmd.TargetOffset.DeltaX);
            Assert.AreEqual(-2.5f, cmd.TargetOffset.DeltaZ);
        }

        [Test]
        public void Offset_Properties_AreImmutable()
        {
            var offset = new GridMovementCommand.Offset(2.0f, 3.0f);

            // DeltaX and DeltaZ have private setters, so cannot be set here.
            Assert.AreEqual(2.0f, offset.DeltaX);
            Assert.AreEqual(3.0f, offset.DeltaZ);
        }

        [Test]
        public void Offset_Zero_Static_IsNotPubliclySettable()
        {
            // The following line should not compile if uncommented:
            // GridMovementCommand.Offset.Zero = new GridMovementCommand.Offset(5, 5);
            Assert.DoesNotThrow(() =>
            {
                var zero = GridMovementCommand.Offset.Zero;
            });
        }

        [Test]
        public void Cancelled_Event_IsRaised_AndUnsubscribed()
        {
            var cmd = new GridMovementCommand(new GridPosition(0, 0));
            bool eventRaised = false;
            cmd.Cancelled += (s, e) => eventRaised = true;

            cmd.Cancel();
            Assert.IsTrue(eventRaised);

            eventRaised = false;
            cmd.Cancel(); // Should not raise again
            Assert.IsFalse(eventRaised);
        }

        [Test]
        public void Finished_Event_IsRaised_AndUnsubscribed()
        {
            var cmd = new GridMovementCommand(new GridPosition(0, 0));
            bool eventRaised = false;
            cmd.Finished += (s, e) => eventRaised = true;

            cmd.ReportFinished();
            Assert.IsTrue(eventRaised);

            eventRaised = false;
            cmd.ReportFinished(); // Should not raise again
            Assert.IsFalse(eventRaised);
        }

        [Test]
        public void Extra_Property_CanBeSetAndRetrieved()
        {
            var cmd = new GridMovementCommand(new GridPosition(0, 0));
            var extra = new object();
            cmd.Extra = extra;

            Assert.AreSame(extra, cmd.Extra);
        }

        [Test]
        public void MultipleCancelAndFinishCalls_DoNotThrow_AndEventsOnlyRaisedOnce()
        {
            // Test Cancel path
            var cmd1 = new GridMovementCommand(new GridPosition(0, 0));
            int cancelCount1 = 0, finishCount1 = 0;
            cmd1.Cancelled += (s, e) => cancelCount1++;
            cmd1.Finished += (s, e) => finishCount1++;

            cmd1.Cancel();
            cmd1.Cancel(); // Should not throw or raise again
            cmd1.ReportFinished(); // Should not raise Finished after Cancel
            cmd1.ReportFinished();

            Assert.AreEqual(1, cancelCount1, "Cancelled event should be raised once.");
            Assert.AreEqual(0, finishCount1, "Finished event should not be raised after Cancel.");

            // Test Finished path
            var cmd2 = new GridMovementCommand(new GridPosition(0, 0));
            int cancelCount2 = 0, finishCount2 = 0;
            cmd2.Cancelled += (s, e) => cancelCount2++;
            cmd2.Finished += (s, e) => finishCount2++;

            cmd2.ReportFinished();
            cmd2.ReportFinished(); // Should not throw or raise again
            cmd2.Cancel(); // Should not raise Cancelled after Finished
            cmd2.Cancel();

            Assert.AreEqual(1, finishCount2, "Finished event should be raised once.");
            Assert.AreEqual(0, cancelCount2, "Cancelled event should not be raised after Finished.");
        }
    }
}
