#nullable enable
using Codice.CM.Client.Gui;
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
        private static void Shuffle<T>(IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

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

        public List<GridMovementCommand>? FindRandomPathToExit(GridPosition location)
        {
            return RandomDFS(location, t => IsExit(t.Item1));
        }

        public bool IsDrinkingPlace(GridPosition pos)
        {
            return AdjacentCells(pos).Any(t => (map.FieldAt(t.Item1) is Water));
        }

        public bool IsFeedingSite(GridPosition pos)
        {
            return map.FieldAt(pos) is Grass;
        }

        public bool IsExit(GridPosition pos)
        {
            return map.FieldAt(pos) is Exit;
        }

        public void FindEnterenceAndExit()
        {
            for (int i = 0; i < map.SizeX; ++i)
            {
                for (int j = 0; j < map.SizeZ; ++j)
                {
                    if (map.Fields[i, j] is Entrance)
                    {
                        map.EntranceCoords = new GridPosition(i, j);

                    }
                    if (map.Fields[i, j] is Exit)
                    {
                        map.ExitCoords = new GridPosition(i, j);

                    }
                }
            }
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

        private List<GridMovementCommand>? RandomDFS(GridPosition start, Func<Tuple<GridPosition, Offset>, bool> predicate)
        {
            Stack<GridPosition> stack = new Stack<GridPosition>();
            stack.Push(start);

            HashSet<GridPosition> visited = new HashSet<GridPosition>();
            visited.Add(start);

            Dictionary<GridPosition, GridPosition> parent = new Dictionary<GridPosition, GridPosition>();

            while (stack.Count > 0)
            {
                GridPosition currentVertex = stack.Pop();

                var neighbors = WalkableAdjacentCells(currentVertex, true, true).ToList();
                Shuffle(neighbors);

                foreach (var neighbor in neighbors)
                {
                    if (visited.Contains(neighbor.Item1))
                    {
                        continue;
                    }

                    visited.Add(neighbor.Item1);
                    parent[neighbor.Item1] = currentVertex;

                    if (predicate(neighbor))
                    {
                        return ReconstructPath(parent, start, neighbor.Item1);
                    }

                    stack.Push(neighbor.Item1);
                }
            }

            return null;
        }

        private List<GridMovementCommand> ReconstructPath(Dictionary<GridPosition, GridPosition> parent, GridPosition start, GridPosition end)
        {
            List<GridMovementCommand> path = new List<GridMovementCommand>();
            GridPosition current = end;

            while (!current.Equals(start))
            {
                path.Add(new GridMovementCommand(current));
                current = parent[current];
            }
            path.Reverse();
            return path;
        }

        private List<Tuple<GridPosition, Offset>> AdjacentCells(GridPosition pos, bool forbidDiagonal)
        {
            List<Tuple<GridPosition, Offset>> adjacent = new List<Tuple<GridPosition, Offset>>();
            for (int x = -1; x <= 1; x++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    if (x == 0 && z == 0)
                        continue;

                    if (forbidDiagonal && Math.Abs(x) + Math.Abs(z) == 2)
                        continue;

                    GridPosition adjacentCell = new GridPosition(pos.X + x, pos.Z + z);
                    if (map.IsValidPosition(adjacentCell))
                    {
                        adjacent.Add(new Tuple<GridPosition, Offset>(adjacentCell, new Offset(x, z)));
                    }
                }
            }
            return adjacent;
        }

        private List<Tuple<GridPosition, Offset>> AdjacentCells(GridPosition pos)
        {
            return AdjacentCells(pos, false);
        }

        private List<Tuple<GridPosition, Offset>> WalkableAdjacentCells(GridPosition pos, bool forbidDiagonal, bool onlyRoad)
        {
            Trace.Assert(map.IsValidPosition(pos), "Invalid position");
            Trace.Assert(IsWalkable(pos), "Cell not walkable");

            return AdjacentCells(pos, forbidDiagonal).Where(t => IsWalkable(t.Item1, onlyRoad)).ToList();
        }

        private List<Tuple<GridPosition, Offset>> WalkableAdjacentCells(GridPosition pos)
        {
            return WalkableAdjacentCells(pos, false, false);
        }

        private bool IsWalkable(GridPosition pos, bool onlyRoad)
        {
            var field = map.FieldAt(pos);

            if (onlyRoad)
            {
                return field is Road || field is Entrance || field is Exit;
            }

            return field is Road || field is Ground || field is Grass || field is Entrance || field is Exit;
        }

        private bool IsWalkable(GridPosition pos)
        {
            return IsWalkable(pos, false);
        }
    }
}
