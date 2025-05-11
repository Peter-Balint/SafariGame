using Safari.Model.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



namespace Safari.Model
{
    public class MapGenerationHelper
    {
		public static List<GridPosition> GenerateRandomPath(GridPosition start, GridPosition end, int sizeX, int sizeY)
		{
			List<GridPosition> finalPath = new List<GridPosition>();
			bool[,] visited = new bool[sizeY, sizeX];

			bool found = DFS(start, end, sizeX, sizeY, visited, finalPath);
			return found ? finalPath : new List<GridPosition>();
		}

		private static bool DFS(GridPosition current, GridPosition end, int sizeX, int sizeY, bool[,] visited, List<GridPosition> path)
		{
			if (!IsInBounds(current, sizeX, sizeY) || visited[current.Z, current.X])
				return false;

			visited[current.Z, current.X] = true;
			path.Add(current);

			if (current.Equals(end))
				return true;


			int dx = end.X - current.X;
			int dz = end.Z - current.Z;
			List<GridPosition> directions = new List<GridPosition>();

			if (dx != 0)
				directions.Add(new GridPosition(current.X + Math.Sign(dx), current.Z));
			if (dz != 0)
				directions.Add(new GridPosition(current.X, current.Z + Math.Sign(dz)));

			//List<GridPosition> directions = new List<GridPosition>
			//{
			//	new GridPosition(current.X + 1, current.Z),
			//	new GridPosition(current.X - 1, current.Z),
			//	new GridPosition(current.X, current.Z + 1),
			//	new GridPosition(current.X, current.Z - 1)
			//};

			// Shuffle directions randomly
			for (int i = directions.Count - 1; i > 0; i--)
			{
				int j = UnityEngine.Random.Range(0, i + 1);
				(directions[i], directions[j]) = (directions[j], directions[i]);
			}

			foreach (var dir in directions)
			{
				if (DFS(dir, end, sizeX, sizeY, visited, path))
					return true;
			}

			// Backtrack
			path.RemoveAt(path.Count - 1);
			return false;





		}

		public static void FillWithNature(Field[,] grid)
		{
			int sizeY = grid.GetLength(0);
			int sizeX = grid.GetLength(1);

			for (int y = 0; y < sizeY; y++)
			{
				for (int x = 0; x < sizeX; x++)
				{
					Field field = grid[y, x];
					if (field is Road || field is Entrance || field is Exit)
						continue;

					float roll = UnityEngine.Random.value;

					if (roll < 0.4f)
						grid[y, x] = new Grass(BuildingMetadata.Default());
					if (roll < 0.2f)
						grid[y, x] = new Map.Tree(BuildingMetadata.Default());
					if (roll < 0.3f)
						grid[y, x] = new Bush(BuildingMetadata.Default());
				}
			}
		}

		public static void AddWaterPatches(Field[,] grid)
		{
			int sizeZ = grid.GetLength(0);
			int sizeX = grid.GetLength(1);

			for (int z = 0; z < sizeZ; z++)
			{
				for (int x = 0; x < sizeX; x++)
				{
					Field current = grid[z, x];

					if (current is Road || current is Entrance || current is Exit)
						continue;

					// 10% chance to place a water tile
					if (UnityEngine.Random.value < 0.05f)
					{
						grid[z, x] = new Water(BuildingMetadata.Default());

						// Check all 8 neighbors
						for (int dz = -1; dz <= 1; dz++)
						{
							for (int dx = -1; dx <= 1; dx++)
							{
								if (dx == 0 && dz == 0)
									continue;

								TryMakeWater(grid, x + dx, z + dz);
							}
						}
					}
				}
			}
		}

		private static void TryMakeWater(Field[,] grid, int x, int z)
		{
			int sizeZ = grid.GetLength(0);
			int sizeX = grid.GetLength(1);

			if (x < 0 || x >= sizeX || z < 0 || z >= sizeZ)
				return;

			Field neighbor = grid[z, x];

			if (neighbor is Road || neighbor is Entrance || neighbor is Exit)
				return;

			// 50% chance to convert neighbor to water
			if (UnityEngine.Random.value < 0.3f)
			{
				grid[z, x] = new Water(BuildingMetadata.Default());
			}
		}

		private static bool IsInBounds(GridPosition pos, int width, int height)
		{
			return pos.X >= 0 && pos.X < width && pos.Z >= 0 && pos.Z < height;
		}


	}
}
