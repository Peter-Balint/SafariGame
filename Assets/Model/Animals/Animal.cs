
#nullable enable
using Safari.Model.Animals.State;
using Safari.Model.Map;
using Safari.Model.Movement;
using Safari.Model.Pathfinding;
using System;
using System.Resources;
using UnityEditor;
using UnityEngine;

namespace Safari.Model.Animals
{
    public abstract class Animal
    {
        public MovementBehavior Movement { get; }

        public PathfindingHelper Pathfinding { get; }

        public State.State State { get; private set; }

        public Vector3 Position { get; set; }

        public Tuple<float, float> RestingInterval { get; private set; }

        public int ThirstLimit { get; private set; }

        public int CriticalThirstLimit { get; private set; }

        public int DrinkingRate { get; private set; }

        public int HungerLimit { get; private set; }

        public int CriticalHungerLimit { get; private set; }

        public int EatingRate { get; private set; }

        protected AnimalMetadata metadata;

        protected int age;
        protected bool isAdult;
        protected Gender gender;

        //for setting from the editor, arbitrary numbers for now
        public const int lifeSpan = 50000;

        //animals should move in a group: an easy solution would be to designate a leader
        //the animals with that leader have a bias to move towards them while in wandering state
        //if leader is null this animal has no leader / is a leader
        //public Animal? leader; //could be put in a child class to make sure it is of the same type

        public event EventHandler? Died;
        public event EventHandler? StateChanged;


        protected Animal(PathfindingHelper pathfinding)
        {
            Movement = new MovementBehavior();
            age = 0;
            ThirstLimit = 10000;
            CriticalThirstLimit = 20000;
            DrinkingRate = 2500;
            HungerLimit = 30000;
            CriticalHungerLimit = 50000;
            EatingRate = 4000;
            RestingInterval = new Tuple<float, float>(0.05f * 600, 0.1f * 600);
            State = new State.Resting(this, 0, 0);
            State.OnEnter();
            Pathfinding = pathfinding;
            metadata = new AnimalMetadata();
        }

        protected Animal(PathfindingHelper pathfinding, AnimalMetadata metadata)
        {
            Movement = new MovementBehavior();
            age = 0;
            ThirstLimit = 10000;
            CriticalThirstLimit = 20000;
            DrinkingRate = 2500;
            HungerLimit = 30000;
            CriticalHungerLimit = 50000;
            EatingRate = 4000;
            RestingInterval = new Tuple<float, float>(0.05f * 600, 0.1f * 600);
            State = new State.Resting(this, 0, 0);
            State.OnEnter();
            Pathfinding = pathfinding;
            this.metadata = metadata;
        }

        internal void SetState(State.State state)
        {
            State.OnExit();
            State = state;
            State.OnEnter();
            StateChanged?.Invoke(this, EventArgs.Empty);
            if (State is Dead)
            {
                Died?.Invoke(this, EventArgs.Empty);
            }
        }

        public void ModelUpdate(float deltaTime, int speedFactor)
        {
            State.Update(deltaTime, speedFactor);
        }
        public void TargetReached()
        {
        }

        public abstract State.State HandleFoodFinding();
    }

    public enum AnimalState
    {
        //states for pathfinding: resting should stay in place for a while
        //wandering should aim for a relatively random position
        //hungry and thirst should look for the closest food/water source
        Resting, Wandering, Hungry, Thirsty
    }
    public enum Gender
    {
        Female, Male
    }
}
