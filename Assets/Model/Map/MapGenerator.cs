using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Map
{
    public static class MapGenerator
    {
        public static Map GenerateMap(int sizeX, int sizeY)
        {
            Trace.Assert(sizeX > 3);
            Trace.Assert(sizeY > 3);
            Field[,] grid = new Field[sizeY, sizeX];
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    grid[y, x] = new Ground();
                }
            }
            grid[0, 1] = new Gate();
            grid[sizeY - 1, sizeX - 2] = new Gate();
            return new Map(grid, new GridPosition(1, 0), new GridPosition(sizeX - 2, sizeY - 1));
        }
    }
}
