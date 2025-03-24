using Mono.Cecil;
using Safari.Model.Map;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Pathfinding
{
    public class PathfindingHelper
    {
        private Map.Map map;

        public PathfindingHelper(Map.Map map)
        {
            this.map = map;
        }

        public GridPosition? FindClosestDrinkingPlace(GridPosition location)
        {
            Queue<GridPosition> q = new Queue<GridPosition>();
            q.Enqueue(location);
            while (q.Count > 0)
            {
                GridPosition currentVertex = q.Dequeue();
                foreach (var neighbor in WalkableAdjacentCells(currentVertex))
                {
                    if (IsDrinkingPlace(neighbor))
                    {
                        return neighbor;
                    }
                    q.Enqueue(neighbor);
                }
            }
            return null;
        }

        private bool IsDrinkingPlace(GridPosition pos)
        {
            return AdjacentCells(pos).Any(cell => (map.FieldAt(cell) is Water));
        }

        private List<GridPosition> AdjacentCells(GridPosition pos)
        {
            List<GridPosition> adjacent = new List<GridPosition>();
            for (int x = -1; x <= 1; x++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    GridPosition adjacentCell = new GridPosition(pos.X + x, pos.Z + z);
                    if (map.IsValidPosition(adjacentCell))
                    {
                        adjacent.Add(adjacentCell);
                    }
                }
            }
            return adjacent;
        }

        private List<GridPosition> WalkableAdjacentCells(GridPosition pos)
        {
            Trace.Assert(map.IsValidPosition(pos), "Invalid position");
            Trace.Assert(IsWalkable(pos), "Cell not walkable");

            return AdjacentCells(pos).Where(IsWalkable).ToList();
        }

        private bool IsWalkable(GridPosition pos)
        {
            return map.FieldAt(pos) is Road || map.FieldAt(pos) is Ground;
        }
    }
}
