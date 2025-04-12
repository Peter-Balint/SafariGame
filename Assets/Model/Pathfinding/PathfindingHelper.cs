#nullable enable
using Mono.Cecil;
using Safari.Model.Map;
using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Safari.Model.Movement.GridMovementCommand;

namespace Safari.Model.Pathfinding
{
    public class PathfindingHelper
    {
        private Map.Map map;

        public PathfindingHelper(Map.Map map)
        {
            this.map = map;
        }

        public GridMovementCommand? FindClosestDrinkingPlace(GridPosition location)
        {
            return BFS(location, t => IsDrinkingPlace(t.Item1));
        }

        public GridMovementCommand? FindClosestFeedingSite(GridPosition location)
        {
            return BFS(location, t => IsFeedingSite(t.Item1));

        }

        public bool IsDrinkingPlace(GridPosition pos)
        {
            return AdjacentCells(pos).Any(t => (map.FieldAt(t.Item1) is Water));
        }

        public bool IsFeedingSite(GridPosition pos)
        {
            return map.FieldAt(pos) is Grass;
        }

        private GridMovementCommand? BFS(GridPosition start, Func<Tuple<GridPosition, Offset>, bool> predicate)
        {
            Queue<GridPosition> q = new Queue<GridPosition>();
            q.Enqueue(start);

            HashSet<GridPosition> visited = new HashSet<GridPosition>();

            while (q.Count > 0)
            {
                GridPosition currentVertex = q.Dequeue();
                foreach (var neighbor in WalkableAdjacentCells(currentVertex))
                {
                    if (visited.Contains(neighbor.Item1))
                    {
                        continue;
                    }
                    visited.Add(neighbor.Item1);
                    if (predicate(neighbor))
                    {
                        return new GridMovementCommand(neighbor.Item1, neighbor.Item2);
                    }
                    q.Enqueue(neighbor.Item1);
                }
            }
            return null;
        }

        private List<Tuple<GridPosition, Offset>> AdjacentCells(GridPosition pos)
        {
            List<Tuple<GridPosition, Offset>> adjacent = new List<Tuple<GridPosition, Offset>>();
            for (int x = -1; x <= 1; x++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    GridPosition adjacentCell = new GridPosition(pos.X + x, pos.Z + z);
                    if (map.IsValidPosition(adjacentCell))
                    {
                        adjacent.Add(new Tuple<GridPosition, Offset>(adjacentCell, new Offset(x, z)));
                    }
                }
            }
            return adjacent;
        }

        private List<Tuple<GridPosition, Offset>> WalkableAdjacentCells(GridPosition pos)
        {
            Trace.Assert(map.IsValidPosition(pos), "Invalid position");
            Trace.Assert(IsWalkable(pos), "Cell not walkable");

            return AdjacentCells(pos).Where(t => IsWalkable(t.Item1)).ToList();
        }

        private bool IsWalkable(GridPosition pos)
        {
            return map.FieldAt(pos) is Road || map.FieldAt(pos) is Ground;
        }
    }
}
