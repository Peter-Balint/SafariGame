
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

        public State.State State { get; private set; }

        public AnimalMetadata Metadata { get { return metadata; } }

        public Group Group { get; set; }
        
        public Gender gender { get; }

        protected AnimalMetadata metadata;

        public event EventHandler? Died;
        public event EventHandler? StateChanged;

        protected Animal(PathfindingHelper pathfinding, Group group, Vector3 wordPos)
        {
            Movement = new MovementBehavior(this, wordPos);
            State = new State.Resting(this, 100, 100, 0);
            State.OnEnter();
            Pathfinding = pathfinding;
            metadata = new AnimalMetadata();
            Group = group;
            group?.AddAnimal(this);
            gender = (Gender)UnityEngine.Random.Range(0, 2);
        }

        protected Animal(PathfindingHelper pathfinding, AnimalMetadata metadata, Group group, Vector3 wordPos)
        {
            Movement = new MovementBehavior(this, wordPos);
            State = new State.Resting(this, 100, 100, 0);
            State.OnEnter();
            Pathfinding = pathfinding;
            this.metadata = metadata;
            Group = group;
            group?.AddAnimal(this);
            gender = (Gender)UnityEngine.Random.Range(0, 2);
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

        public abstract State.State HandleFoodFinding();
    }

    public enum Gender
    {
        Female, Male
    }
}
