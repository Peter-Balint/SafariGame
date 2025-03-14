using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model
{
    public abstract class Field
    {
        public abstract bool CanDemolish { get; }

        public abstract bool CanPlaceHere(Ground field);

        public abstract bool CanPlaceHere(Plant field);

        public abstract bool CanPlaceHere(Road field);

        public abstract bool CanPlaceHere(Water field);

        public abstract bool CanPlaceHere(Gate field);

    }
}
