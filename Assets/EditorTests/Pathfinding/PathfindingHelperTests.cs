using NUnit.Framework;
using Safari.Model.Pathfinding;
using Safari.Model.Map;
using Safari.Model.Movement;
using System.Collections.Generic;

namespace EditorTests.Pathfinding
{
    // Minimal mock field types for testing
    [TestFixture]
    public class PathfindingHelperTests
    {
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

        [Test]
        public void FindClosestDrinkingPlace_HappyScenario_FindsWater()
        {
            var map = CreateSimpleMap(out var entrance, out var exit);
            var helper = new PathfindingHelper(map);

            // Start at (1,1) which is adjacent to (2,0) Water
            var result = helper.FindClosestDrinkingPlace(new GridPosition(1, 1));
            Assert.IsNotNull(result);
            // finds closest shore 
            Assert.AreEqual(new GridPosition(1, 0), result.TargetCell);
        }

        [Test]
        public void FindClosestFeedingSite_HappyScenario_FindsGrass()
        {
            var map = CreateSimpleMap(out var entrance, out var exit);
            var helper = new PathfindingHelper(map);

            // Start at (0,2) which is adjacent to (1,1) Grass
            var result = helper.FindClosestFeedingSite(new GridPosition(0, 2));
            Assert.IsNotNull(result);
            Assert.AreEqual(new GridPosition(1, 1), result.TargetCell);
        }

        [Test]
        public void FindRandomPathToExit_HappyScenario_FindsPath()
        {
            var map = CreateSimpleMap(out var entrance, out var exit);
            var helper = new PathfindingHelper(map);

            // Start at (0,2), path to (2,2) exists
            var result = helper.FindRandomPathToExit(new GridPosition(0, 2));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            Assert.AreEqual(new GridPosition(2, 2), result[result.Count - 1].TargetCell);
        }

        [Test]
        public void FindEntrance_HappyScenario_FindsEntrance()
        {
            var map = CreateSimpleMap(out var entrance, out var exit);
            var helper = new PathfindingHelper(map);

            // Start at (2,2), should find entrance at (0,0)
            var result = helper.FindEntrance(new GridPosition(2, 2));
            Assert.IsNotNull(result);
            Assert.AreEqual(entrance, result.TargetCell);
        }

        [Test]
        public void IsDrinkingPlace_IsFeedingSite_IsExit_HappyScenario()
        {
            var map = CreateSimpleMap(out var entrance, out var exit);
            var helper = new PathfindingHelper(map);

            Assert.IsTrue(helper.IsDrinkingPlace(new GridPosition(1, 1))); // Adjacent to water at (2,0)
            Assert.IsTrue(helper.IsFeedingSite(new GridPosition(1, 1)));   // On grass
            Assert.IsTrue(helper.IsExit(new GridPosition(2, 2)));          // On exit
        }

        [Test]
        public void NoPathExists_ReturnsNull()
        {
            // All cells are Water except entrance
            var fields = new Field[2, 2];
            fields[0, 0] = new Entrance(); // (x=0, z=0)
            fields[0, 1] = new Water();    // (x=1, z=0)
            fields[1, 0] = new Water();    // (x=0, z=1)
            fields[1, 1] = new Water();    // (x=1, z=1)
            var map = new Map(fields, new GridPosition(0, 0), new GridPosition(1, 1));
            var helper = new PathfindingHelper(map);

            Assert.IsNull(helper.FindClosestDrinkingPlace(new GridPosition(0, 0)));
            Assert.IsNull(helper.FindClosestFeedingSite(new GridPosition(0, 0)));
            Assert.IsNull(helper.FindRandomPathToExit(new GridPosition(0, 0)));
        }

        [Test]
        public void MultipleEquidistantTargets_ReturnsOneOfThem()
        {
            // Two grass tiles at same distance from (1,1)
            var fields = new Field[3, 3];
            fields[1, 1] = new Road();     // (x=1, z=1)
            fields[0, 1] = new Grass();    // (x=1, z=0)
            fields[2, 1] = new Grass();    // (x=1, z=2)
            for (int z = 0; z < 3; z++)
                for (int x = 0; x < 3; x++)
                    if (fields[z, x] == null) fields[z, x] = new Road();

            var map = new Map(fields, new GridPosition(1, 1), new GridPosition(2, 2));
            var helper = new PathfindingHelper(map);

            var result = helper.FindClosestFeedingSite(new GridPosition(1, 1));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.TargetCell.Equals(new GridPosition(1, 0)) || result.TargetCell.Equals(new GridPosition(1, 2)));
        }

        [Test]
        public void RandomPathToExit_MultipleBranches_AlwaysFindsExit()
        {
            // Map with two possible paths to exit
            var fields = new Field[3, 3];
            fields[0, 0] = new Entrance(); // (x=0, z=0)
            fields[0, 1] = new Road();     // (x=1, z=0)
            fields[0, 2] = new Road();     // (x=2, z=0)
            fields[1, 0] = new Road();     // (x=0, z=1)
            fields[1, 1] = new Road();     // (x=1, z=1)
            fields[1, 2] = new Road();     // (x=2, z=1)
            fields[2, 0] = new Road();     // (x=0, z=2)
            fields[2, 1] = new Road();     // (x=1, z=2)
            fields[2, 2] = new Exit();     // (x=2, z=2)

            var map = new Map(fields, new GridPosition(0, 0), new GridPosition(2, 2));
            var helper = new PathfindingHelper(map);

            var result = helper.FindRandomPathToExit(new GridPosition(0, 0));
            Assert.IsNotNull(result);
            Assert.AreEqual(new GridPosition(2, 2), result[result.Count - 1].TargetCell);
        }

        [Test]
        public void StartIsAtTarget_ReturnsImmediateResult()
        {
            var fields = new Field[1, 1];
            fields[0, 0] = new Grass(); // (x=0, z=0)
            var map = new Map(fields, new GridPosition(0, 0), new GridPosition(0, 0));
            var helper = new PathfindingHelper(map);

            Assert.IsTrue(helper.IsFeedingSite(new GridPosition(0, 0)));
            var result = helper.FindClosestFeedingSite(new GridPosition(0, 0));
            Assert.IsNull(result); // Because BFS skips the start cell itself
        }

        [Test]
        public void DiagonalMovementForbidden_PathIsOrthogonal()
        {
            // Only orthogonal movement allowed
            var fields = new Field[1, 3];
            fields[0, 0] = new Entrance(); // (x=0, z=0)
            fields[0, 1] = new Road();     // (x=1, z=0)
            fields[0, 2] = new Exit();     // (x=2, z=0)
            var map = new Map(fields, new GridPosition(0, 0), new GridPosition(2, 0));
            var helper = new PathfindingHelper(map);

            var result = helper.FindEntrance(new GridPosition(2, 0));
            Assert.IsNotNull(result);
            Assert.AreEqual(new GridPosition(0, 0), result.TargetCell);
        }

        [Test]
        public void OnlyRoadTraversal_DoesNotTraverseGrassOrGround()
        {
            var fields = new Field[1, 3];
            fields[0, 0] = new Entrance(); // (x=0, z=0)
            fields[0, 1] = new Grass();    // (x=1, z=0)
            fields[0, 2] = new Exit();     // (x=2, z=0)
            var map = new Map(fields, new GridPosition(0, 0), new GridPosition(2, 0));
            var helper = new PathfindingHelper(map);

            // Should not be able to reach exit, as only road/entrance/exit are allowed
            var result = helper.FindEntrance(new GridPosition(2, 0));
            Assert.IsNull(result);
        }

        [Test]
        public void JunctionDetectionInPathReconstruction_AddsCommandsAtJunctions()
        {
            // Path: (0,0) -> (1,0) -> (1,1)
            var fields = new Field[2, 2];
            fields[0, 0] = new Entrance(); // (x=0, z=0)
            fields[0, 1] = new Road();     // (x=1, z=0)
            fields[1, 0] = new Road();     // (x=0, z=1)
            fields[1, 1] = new Exit();     // (x=1, z=1)
            var map = new Map(fields, new GridPosition(0, 0), new GridPosition(1, 1));
            var helper = new PathfindingHelper(map);

            var result = helper.FindRandomPathToExit(new GridPosition(0, 0));
            Assert.IsNotNull(result);
            // There should be a command for the junction at (1,0) -> (1,1)
            Assert.IsTrue(result.Exists(cmd => cmd.TargetCell.Equals(new GridPosition(1, 1))));
        }
    }
}
