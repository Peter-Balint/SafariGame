
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
    public abstract class Animal : IMoving
    {
        public MovementBehavior Movement { get; }

        public PathfindingHelper Pathfinding { get; }

        public AnimalCollection AnimalCollection { get; } 

        public State.State State { get; private set; }

        public AnimalMetadata Metadata { get { return metadata; } }

        public Group Group { get; set; }
        
        public Gender gender { get; }

        protected AnimalMetadata metadata;

        public event EventHandler? Died;
        public event EventHandler? StateChanged;

        protected Animal(PathfindingHelper pathfinding, Group group, AnimalCollection collection, Vector3 wordPos)
        {
            Movement = new MovementBehavior(this, wordPos);
            AnimalCollection = collection;
            metadata = new AnimalMetadata();
            Pathfinding = pathfinding;
            Group = group;
            group?.AddAnimal(this);
            gender = (Gender)UnityEngine.Random.Range(0, 2);
            State = new State.Resting(this, 100, 100, 0);
            State.OnEnter();
        }

        protected Animal(PathfindingHelper pathfinding, AnimalMetadata metadata, Group group, AnimalCollection collection, Vector3 wordPos)
        {
            Movement = new MovementBehavior(this, wordPos);
            AnimalCollection = collection;
            this.metadata = metadata;
            Pathfinding = pathfinding;
            Group = group;
            group?.AddAnimal(this);
            gender = (Gender)UnityEngine.Random.Range(0, 2);
            State = new State.Resting(this, 100, 100, 0);
            State.OnEnter();
        }

        protected Animal(PathfindingHelper pathfinding, AnimalMetadata metadata, Group group, AnimalCollection collection, MovementBehavior movementBehavior, Vector3 wordPos)
        {
            Movement = movementBehavior;
            AnimalCollection = collection;
            this.metadata = metadata;
            Pathfinding = pathfinding;
            Group = group;
            group?.AddAnimal(this);
            gender = (Gender)UnityEngine.Random.Range(0, 2);
            State = new State.Resting(this, 100, 100, 0);
            State.OnEnter();
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

        internal void InterruptState(State.State newState)
        {
            State.OnInterrupted();
            SetState(newState);
        }

        public void ModelUpdate(float deltaTime, int speedFactor)
        {
            State.Update(deltaTime, speedFactor);
        }

        public void TargetReached()
        {
        }

        public void Kill()
        {
            InterruptState(new Dead(this, State.HydrationPercent, State.SaturationPercent, State.BreedingCooldown));
        }

        public void OnApproachedByMate(Animal mate)
        {
            if (!State.TransitionToMateAllowed())
            {
                return;
            }
            InterruptState(new Mating(this, State.HydrationPercent, State.SaturationPercent, State.BreedingCooldown, mate));
        }

        public abstract State.State HandleFoodFinding();

        public abstract Animal OffspringFactory();

    }

    public enum Gender
    {
        Female, Male
    }
}
