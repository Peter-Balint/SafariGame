
using System;
using System.Resources;
using UnityEngine;
#nullable enable

namespace Safari.Model
{
    public abstract class Animal
    {
        protected AnimalState state = AnimalState.Wandering;
        protected int age;
        protected int hunger;
        protected int thirst;

        //for easy setting from the editor
        public const int lifeSpan = 50000;
        public const int hungerLimit = 1000;
        public const int thirstLimit = 800;

        public Vector3Int Position { get; set; } //could check here in the setter for out of bounds target?

        //animals should move in a group: an easy solution would be to designate a leader
        //the animals with that leader have a bias to move towards them while in wandering state
        //if leader is null this animal has no leader / is a leader
        public Animal? leader;

        public event EventHandler? AnimalDied;
        public event EventHandler? AnimalStateChanged;

        public Animal(Animal? leader)
        {
            this.leader = leader;
            age = 0;
            hunger = 0;
            thirst = 0;
        }

        public void ModelUpdate() //should be called from the view's update function?
        {
            age++;
            hunger++;
            thirst++;

            if(age >= lifeSpan)
            {
                AnimalDied?.Invoke(this, EventArgs.Empty);
                return;
            }
            if (hunger >= hungerLimit && (state == AnimalState.Wandering || state == AnimalState.Resting))
            {
                state = AnimalState.Hungry;
                AnimalStateChanged?.Invoke(this, EventArgs.Empty);
            }
            if(thirst >= thirstLimit && (state == AnimalState.Wandering || state == AnimalState.Resting))
            {
                state = AnimalState.Thirsty;
                AnimalStateChanged?.Invoke(this, EventArgs.Empty); //if pathfinding goes to the view this event is needed
                                                                    //so it can figure out the next target
            }
        }
        public void TargetReached()
        {
            switch (state) 
            {
                case AnimalState.Hungry:
                    {
                        state = AnimalState.Resting;
                        break;
                    }
                case AnimalState.Thirsty:
                    {
                        state = AnimalState.Wandering;
                        AnimalStateChanged?.Invoke(this, EventArgs.Empty);
                        break;
                    }
                case AnimalState.Wandering:
                    {

                        break;
                    }
            }
        }
    }

    public enum AnimalState
    {
        //states for pathfinding: resting should stay in place for a while
        //wandering should aim for a relatively random position
        //hungry and thirst should look for the closest food/water source
        Resting, Wandering, Hungry, Thirsty
    }
}
