using System;
using UnityEngine;

namespace Safari.Model.Animals
{
    [Serializable]
    public class AnimalMetadata
    {
        public static AnimalMetadata Default => new AnimalMetadata()
        {
            Price = 0,
            Value = 0
        };

        public int Price;
        public int Value;

    }
}

