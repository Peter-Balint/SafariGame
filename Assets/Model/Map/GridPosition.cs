using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Map
{
    public struct GridPosition : IEquatable<GridPosition>
    {
        public int X;

        public int Z;

        public GridPosition(int x, int z)
        {
            X = x;
            Z = z;
        }

        public bool Equals(GridPosition other)
        {
            return X == other.X && Z == other.Z;
        }
    }
}
