using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.Rendering.RayTracingAccelerationStructure;
using UnityEngine;

namespace Safari.Model.Map
{
    public static class MapGenerator
    {

		public static Map GenerateMap(int sizeX, int sizeY)
        {
            Trace.Assert(sizeX > 3);
            Trace.Assert(sizeY > 3);
            Field[,] grid = new Field[sizeY, sizeX];


			GridPosition entrance = new GridPosition(0, 1);
			GridPosition exit = new GridPosition(sizeY - 1, sizeX - 2);

			

			int seed = 42; // <-- választható seed
			System.Random rng = new System.Random(seed);


			for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    grid[y, x] = new Ground();
                }
            }

			List<GridPosition> path = MapGenerationHelper.GenerateRandomPath(entrance, exit, sizeX, sizeY,rng);
			foreach (var pos in path)
			{
			
				grid[pos.X, pos.Z] = new Road(BuildingMetadata.Default());
			
			}

			//MapGenerationHelper.FillWithNature(grid);
			//MapGenerationHelper.AddWaterPatches(grid);
			MapGenerationHelper.GenerateTerrain(grid,rng, 0.5f);

		   //grid[0, 1] = new Entrance();
		   //grid[sizeY - 1, sizeX - 2] = new Exit();
			grid[entrance.X, entrance.Z] = new Entrance();
			grid[exit.X, exit.Z] = new Exit();
			return new Map(grid, new GridPosition(entrance.Z,entrance.X), new GridPosition(exit.Z, exit.X));
        }
    }
}
