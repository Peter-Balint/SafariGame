
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

        public Vector3Int Position { get; set; }

        //for setting from the editor, arbitrary numbers for now
        public Tuple<float, float> RestingInterval { get; private set; }

        public int DrinkingRate { get; private set; }

        public int HungerLimit { get; private set; }

        public int CriticalHungerLimit { get; private set; }

        public int ThirstLimit { get; private set; }

        public int CriticalThirstLimit { get; private set; }

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
            HungerLimit = 2000;
            CriticalHungerLimit = 5000;
            ThirstLimit = 1000;
            CriticalThirstLimit = 2000;
            DrinkingRate = 50;
            RestingInterval = new Tuple<float, float>(0.05f * 60, 0.1f * 60);
            State = new State.Resting(this, 0, 0);
            State.OnEnter();
            Pathfinding = pathfinding;
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

        public void ModelUpdate(float deltaTime)
        {
            State.Update(deltaTime);
            /*age++;
            hunger++;

            if (!isAdult && age > lifeSpan / 2)
            {
                isAdult = true;
            }
            if (age >= lifeSpan)
            {
                Died?.Invoke(this, EventArgs.Empty);
                return;
            }
            if (hunger >= hungerLimit && state == AnimalState.Resting)
            {
                state = AnimalState.Hungry;
                StateChanged?.Invoke(this, EventArgs.Empty);
            }
            if (thirst >= thirstLimit && (state == AnimalState.Resting || state == AnimalState.Wandering))
            {
                state = AnimalState.Thirsty;
                StateChanged?.Invoke(this, EventArgs.Empty); 
                                                             
                Debug.Log($"{GetType().Name} is thirsty");
                MovementCommand? command = pathfinding.FindClosestDrinkingPlace(Movement.Location);
                
                if (command is MovementCommand c)
                {
                    Debug.Log($"{GetType().Name} found a drinking place at {c.TargetCell.X} {c.TargetCell.Z}");

                    Movement.ExecuteMovement(c);
                }
                else
                {
                    Debug.Log($"{GetType().Name} couldn't find a drinking place");

                }
            }*/
        }
        public void TargetReached()
        {
            /* switch (state)
             {
                 case AnimalState.Hungry:
                     {
                         state = AnimalState.Resting;
                         break;
                     }
                 case AnimalState.Thirsty:
                     {
                         state = AnimalState.Wandering;
                         StateChanged?.Invoke(this, EventArgs.Empty);
                         break;
                     }
                 case AnimalState.Wandering:
                     {
                         state = AnimalState.Resting;
                         break;
                     }
             }*/
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
