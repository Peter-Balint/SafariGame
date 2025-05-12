using NUnit.Framework;
using Safari.Model.Animals;
using Safari.Model.Animals.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorTests.Movement
{
    public class FollowPreyMovementCommandTests
    {
        private class MockPrey : IPrey
        {
            public bool EscapedCalled { get; private set; }
            public void OnChased(Chaser chaser) { }
            public void Kill() { }
            public void OnEscaped() => EscapedCalled = true;
        }

        private class DummyChaser : Chaser
        {
            public DummyChaser() : base(() => default, () => default) { }
        }

        [Test]
        public void StalkingFinished_Event_Fires_Only_Once()
        {
            var cmd = new FollowPreyMovementCommand(1, 2, 3);
            int eventCount = 0;
            cmd.StalkingFinished += (s, e) => eventCount++;

            cmd.ReportPreyNotFound();
            cmd.ReportPreyNotFound();
            Assert.AreEqual(1, eventCount);
            Assert.IsTrue(cmd.IsStalkingFinished);
        }

        [Test]
        public void PreyEscaped_Event_Fires_Only_Once()
        {
            var cmd = new FollowPreyMovementCommand(1, 2, 3);
            int eventCount = 0;
            cmd.PreyEscaped += (s, e) => eventCount++;

            cmd.ReportPreyEscaped();
            cmd.ReportPreyEscaped();
            Assert.AreEqual(1, eventCount);
        }

        [Test]
        public void Prey_Property_Set_On_PreyApproached()
        {
            var cmd = new FollowPreyMovementCommand(1, 2, 3);
            var prey = new MockPrey();
            var chaser = new DummyChaser();
            var result = new PreyApproached(prey, chaser);

            cmd.ReportPreyApproached(result);

            Assert.AreSame(prey, cmd.Prey);
        }

        [Test]
        public void OnEscaped_Called_On_Cancel()
        {
            var cmd = new FollowPreyMovementCommand(1, 2, 3);
            var prey = new MockPrey();
            var chaser = new DummyChaser();
            var result = new PreyApproached(prey, chaser);

            cmd.ReportPreyApproached(result);
            cmd.Cancel();

            Assert.IsTrue(prey.EscapedCalled);
        }

        [Test]
        public void ReportFinished_Called_After_ReportPreyNotFound()
        {
            var cmd = new FollowPreyMovementCommand(1, 2, 3);
            bool finished = false;
            cmd.Finished += (s, e) => finished = true;

            cmd.ReportPreyNotFound();

            Assert.IsTrue(finished);
        }

        [Test]
        public void StalkingFinishedEventArgs_Contains_Correct_Result()
        {
            var cmd = new FollowPreyMovementCommand(1, 2, 3);
            var prey = new MockPrey();
            var chaser = new DummyChaser();
            var result = new PreyApproached(prey, chaser);

            StalkingFinishedEventArgs? receivedArgs = null;
            cmd.StalkingFinished += (s, e) => receivedArgs = e;

            cmd.ReportPreyApproached(result);

            Assert.IsNotNull(receivedArgs);
            Assert.AreSame(result, receivedArgs.Result);
        }

        [Test]
        public void State_Reset_On_New_Instance()
        {
            var cmd1 = new FollowPreyMovementCommand(1, 2, 3);
            var cmd2 = new FollowPreyMovementCommand(1, 2, 3);

            int count1 = 0, count2 = 0;
            cmd1.StalkingFinished += (s, e) => count1++;
            cmd2.StalkingFinished += (s, e) => count2++;

            cmd1.ReportPreyNotFound();
            cmd2.ReportPreyNotFound();

            Assert.AreEqual(1, count1);
            Assert.AreEqual(1, count2);
        }

        [Test]
        public void CanEscape_Property_Behavior()
        {
            var cmd = new FollowPreyMovementCommand(1, 2, 3);
            cmd.CanEscape = true;
            Assert.IsTrue(cmd.CanEscape);
            cmd.CanEscape = false;
            Assert.IsFalse(cmd.CanEscape);
        }

        [Test]
        public void Radius_Properties_Set_By_Constructor()
        {
            var cmd = new FollowPreyMovementCommand(10, 20, 30);
            Assert.AreEqual(10, cmd.StalkingFinishedRadius);
            Assert.AreEqual(20, cmd.FinishedRadius);
            Assert.AreEqual(30, cmd.EscapeRadius);
        }

        [Test]
        public void Cancel_Does_Not_Throw_When_Prey_Is_Null()
        {
            var cmd = new FollowPreyMovementCommand(1, 2, 3);
            Assert.DoesNotThrow(() => cmd.Cancel());
        }
    }
}
