using System;
using UnityEngine;

namespace Safari.Model.Animals
{
    [Serializable]
    public class AnimalMetadata
    {
        // minutes
        public int TimeTillHungry { get; private set; }
        // minutes
        public int TimeTillThirsty { get; private set; }
        // minutes
        public int TimeTillStarvation { get; private set; }
        // minutes
        public int TimeTillDehydration { get; private set; }

        public static AnimalMetadata Default => new AnimalMetadata()
        {
            Price = 0,
            Value = 0
        };

        public int Price;
        public int Value;

    }
}

