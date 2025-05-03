using System;
using UnityEngine;

namespace Safari.Model.Animals
{
    [Serializable]
    public class AnimalMetadata
    {
        public int Price;
        public int Value;
        /// <summary>
        /// Minutes
        /// </summary>
        public int TimeTillHungry;
        /// <summary>
        /// Minutes
        /// </summary>
        public int TimeTillThirsty;
        /// <summary>
        /// Minutes
        /// </summary>
        public int TimeTillStarvation;
        /// <summary>
        /// Minutes
        /// </summary>
        public int TimeTillDehydration;
        /// <summary>
        /// Minutes
        /// </summary>
        public int TimeTillFullySaturated;
        /// <summary>
        /// Minutes
        /// </summary>
        public int TimeTillFullyHydrated;
        /// <summary>
        /// Minutes
        /// </summary>
        public int RestingTimeMin;
        /// <summary>
        /// Minutes
        /// </summary>
        public int RestingTimeMax;
        /// <summary>
        /// Days
        /// </summary>
        public int LifeSpan;

        /// <summary>
        /// 0-100
        /// At what hydration percent the animal will start to look for water
        /// </summary>
        public int ThirstyPercent;

        /// <summary>
        /// 0-100
        /// At what hydration percent the animal will search for another water source, if no longer near water
        /// </summary>
        public int StillThirstyPercent;

        public static AnimalMetadata Default => new AnimalMetadata()
        {
            Price = 0,
            Value = 0,
            TimeTillHungry = 3 * 60,
            TimeTillThirsty = 30,
            TimeTillStarvation = 2 * 24 * 60,
            TimeTillDehydration = 12 * 60,
            TimeTillFullySaturated = 20,
            TimeTillFullyHydrated = 1,
            RestingTimeMin = 1 * 60,
            RestingTimeMax = 2 * 60,
            LifeSpan = 10 * 365,
            ThirstyPercent = 10,
            StillThirstyPercent = 30
        };
    }
}

