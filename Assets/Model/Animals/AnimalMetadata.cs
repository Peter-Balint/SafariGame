using System;
using UnityEngine;

namespace Safari.Model.Animals
{
    [Serializable]
    public class AnimalMetadata
    {
        [Tooltip("The price of the animal.")]
        public int Price = 0;

        [Tooltip("The value of the animal.")]
        public int Value = 0;

        /// <summary>
        /// Minutes
        /// </summary>
        [Tooltip("Minutes until the animal becomes hungry.")]
        public int TimeTillHungry = 5 * 60;

        /// <summary>
        /// Minutes
        /// </summary>
        [Tooltip("Minutes until the animal becomes thirsty.")]
        public int TimeTillThirsty = 1 * 60;

        /// <summary>
        /// Minutes
        /// </summary>
        [Tooltip("Minutes until the animal starves.")]
        public int TimeTillStarvation = 2 * 24 * 60;

        /// <summary>
        /// Minutes
        /// </summary>
        [Tooltip("Minutes until the animal becomes dehydrated.")]
        public int TimeTillDehydration = 18 * 60;

        /// <summary>
        /// Minutes
        /// </summary>
        [Tooltip("Minutes until the animal is fully saturated.")]
        public int TimeTillFullySaturated = 20;

        /// <summary>
        /// Minutes
        /// </summary>
        [Tooltip("Minutes until the animal is fully hydrated.")]
        public int TimeTillFullyHydrated = 1;

        /// <summary>
        /// Minutes
        /// </summary>
        [Tooltip("Minimum resting time in minutes.")]
        public int RestingTimeMin = 1 * 60;

        /// <summary>
        /// Minutes
        /// </summary>
        [Tooltip("Maximum resting time in minutes.")]
        public int RestingTimeMax = 2 * 60;

        /// <summary>
        /// Days
        /// </summary>
        [Tooltip("The lifespan of the animal in days.")]
        public int LifeSpan = 10 * 365;

        /// <summary>
        /// 0-100
        /// At what hydration percent the animal will start to look for water
        /// </summary>
        [Tooltip("At what hydration percent (0-100) the animal will start to look for water.")]
        public int ThirstyPercent = 10;

        /// <summary>
        /// 0-100
        /// At what hydration percent the animal will search for another water source, if no longer near water
        /// </summary>
        [Tooltip("At what hydration percent (0-100) the animal will search for another water source if no longer near water.")]
        public int StillThirstyPercent = 30;

        public static AnimalMetadata Default => new AnimalMetadata();
    }
}

