using JetBrains.Annotations;
using System;
using System.Resources;
using UnityEngine;

namespace Safari.Model
{
    public abstract class Animal
    {
        protected AnimalState state = AnimalState.Wandering;
        protected int age;
        protected int hunger;
        protected int thirst;

        public event EventHandler AnimalDied;

        public void ModelUpdate() //should be called from the views update function?
        {
            age++;
            hunger++;
            thirst++;

            if(age >= 10000)
            {
                AnimalDied?.Invoke(this, EventArgs.Empty);
                return;
            }
            if (hunger >= 1000 && (state == AnimalState.Wandering || state == AnimalState.Resting))
            {
                state = AnimalState.Hungry;
            }
            if(thirst >= 800 && (state == AnimalState.Wandering || state == AnimalState.Resting))
            {
                state = AnimalState.Thirsty;
            }
        }
    }

    public enum AnimalState
    {
        Resting, Wandering, Hungry, Thirsty
    }
}
