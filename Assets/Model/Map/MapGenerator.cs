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
            for (int i = 0; i < 5; i++)
            {
                for (global::System.Int32 j = 0; j < 5; j++)
                {
                    grid[j +2 , i+2] = new Water(BuildingMetadata.Default());
                }
            }
            grid[0, 1] = new Entrance();
            grid[sizeY - 1, sizeX - 2] = new Exit();
            return new Map(grid, new GridPosition(1, 0), new GridPosition(sizeX - 2, sizeY - 1));
        }
    }
}
