#nullable enable
using System;
using UnityEngine;

namespace Safari.Model.Map
{
    public class Map
    {
        public event EventHandler<GridPosition>? FieldChanged;

        public GridPosition EntranceCoords { get; }

        public GridPosition ExitCoords { get; }

        public int SizeX => grid.GetLength(1);

        public int SizeZ => grid.GetLength(0);

        private Field[,] grid;

        public Map(Field[,] grid, GridPosition entranceCoords, GridPosition exitCoords)
        {
            this.grid = grid;
            EntranceCoords = entranceCoords;
            ExitCoords = exitCoords;
        }

        internal void ChangeFieldAt(GridPosition position, Field newField)
        {
            if (position.X <0 || position.Z < 0 || position.X >= grid.GetLength(1) || position.Z >= grid.GetLength(0))
            {
                return;
            }
            grid[position.Z, position.X] = newField;
            FieldChanged?.Invoke(this, position);
        }

        public Field FieldAt(GridPosition position)
        {
            return grid[position.Z, position.X];
        }
    }
}

