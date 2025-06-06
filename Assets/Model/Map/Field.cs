﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Map
{
    public abstract class Field
    {
        public abstract bool CanDemolish { get; }

        public BuildingMetadata Metadata { get; }

        protected Field(BuildingMetadata metadata)
        {
            Metadata = metadata;
        }

        public abstract bool CanPlaceHere(Field field);
    }
}
