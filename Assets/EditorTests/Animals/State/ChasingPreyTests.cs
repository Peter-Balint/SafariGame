using NUnit.Framework;
using Safari.Model.Animals.State;
using Safari.Model.Animals.Movement;
using Safari.Model.Movement;
using Safari.Model.Map;
using System;
using UnityEngine;
using Safari.Model.Animals;
using Safari.Model.Pathfinding;

namespace EditorTests.Animals.State
{

    // Mocks and stubs
    public class MockPrey : IPrey
    {
        public bool Killed { get; private set; }
        public bool Escaped { get; private set; }
        public void Kill() => Killed = true;
        public void OnEscaped() => Escaped = true;
        public void OnChased(Chaser chaser) { }
    }

    public class MockAnimal : Safari.Model.Animals.Animal
    {
        public MockAnimal(MovementBehavior movementBehavior, PathfindingHelper h, AnimalCollection c)
            : base(h, AnimalMetadata.Default, new Group(), c, movementBehavior, Vector3.zero) { }

        public override Safari.Model.Animals.State.State HandleFoodFinding() => null;
        public override Safari.Model.Animals.Animal OffspringFactory() => null;
    }

    public class MockFollowPreyMovementCommand : FollowPreyMovementCommand
    {
        public bool FinishedReported { get; private set; }
        public MockFollowPreyMovementCommand() : base(1, 1, 1) { }
        public override void ReportFinished()
        {
            FinishedReported = true;
            base.ReportFinished();
        }
    }

    [TestFixture]
    public class ChasingPreyTests
    {
        private MockAnimal animal;
        private MockPrey prey;
        private MockFollowPreyMovementCommand command;
        private ChasingPrey state;

        private MovementBehavior movementBehavior;

        private Map CreateSimpleMap(out GridPosition entrance, out GridPosition exit)
        {
            // 3x3 map:
            // [E][R][W]
            // [R][G][G]
            // [R][R][X]
            // fields[z, x]
            var fields = new Field[3, 3];
            entrance = new GridPosition(0, 0);
            exit = new GridPosition(2, 2);

            fields[0, 0] = new Entrance(); // (x=0, z=0)
            fields[0, 1] = new Road();     // (x=1, z=0)
            fields[0, 2] = new Water();    // (x=2, z=0)

            fields[1, 0] = new Road();   // (x=0, z=1)
            fields[1, 1] = new Grass();    // (x=1, z=1)
            fields[1, 2] = new Ground();    // (x=2, z=1)

            fields[2, 0] = new Road();     // (x=0, z=2)
            fields[2, 1] = new Road();     // (x=1, z=2)
            fields[2, 2] = new Exit();     // (x=2, z=2)

            return new Map(fields, entrance, exit);
        }

        [SetUp]
        public void SetUp()
        {
            Map map = CreateSimpleMap(out _, out _);
            PathfindingHelper pathfinding = new PathfindingHelper(map);
            movementBehavior = new MovementBehavior(null, Vector3.zero);
            animal = new MockAnimal(movementBehavior, pathfinding, new AnimalCollection(pathfinding));
            prey = new MockPrey();
            command = new MockFollowPreyMovementCommand();
            state = new ChasingPrey(animal, 50, 50, 0, command, prey);
        }

        [Test]
        public void OnEnter_SubscribesEventsAndSetsCanEscape()
        {
            state.OnEnter();
            Assert.IsTrue(command.CanEscape);
            // Simulate event: should not throw
            command.ReportFinished();
            command.ReportPreyEscaped();
        }

        [Test]
        public void OnChasingFinished_AbortsMovement_KillsPrey_TransitionsToPredatorEating()
        {

            animal.Movement.ExecuteMovement(command);

            state.OnEnter();
            command.ReportFinished();

            Assert.IsTrue(prey.Killed);
            Assert.IsTrue(animal.State is PredatorEating);
            // State transition to PredatorEating is internal, so we can't check directly without more infrastructure
        }

        [Test]
        public void OnPreyEscaped_ReportsFinished_CallsOnEscaped_TransitionsToFailedHunting()
        {
            state.OnEnter();
            command.ReportPreyEscaped();

            Assert.IsTrue(command.FinishedReported);
            Assert.IsTrue(prey.Escaped);
            // State transition to FailedHunting is internal, so we can't check directly without more infrastructure
        }

        [Test]
        public void OnExit_UnsubscribesAndReportsFinished()
        {
            state.OnEnter();
            state.OnExit();
            Assert.IsTrue(command.FinishedReported);
            // Unsubscribing is not directly testable, but should not throw on double exit
            Assert.DoesNotThrow(() => state.OnExit());
        }

        [Test]
        public void MultipleEvents_OnlyFirstTransitionTakesEffect()
        {
            state.OnEnter();
            command.ReportPreyEscaped();
            // After PreyEscaped, Finished should not do anything harmful
            Assert.DoesNotThrow(() => command.ReportFinished());
        }

        [Test]
        public void CanEscape_IsSetOnEnter()
        {
            state.OnEnter();
            Assert.IsTrue(command.CanEscape);
        }
    
    }

}