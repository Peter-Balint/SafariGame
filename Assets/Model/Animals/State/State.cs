using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Safari.Model.Animals.State
{
    public abstract class State
    {
        public double HydrationPercent => hydrationPercent;

        public float Hunger => hunger;

        protected double hydrationPercent;

        protected float hunger;

        protected Animal owner;

        private bool transitioned = false;

        private Queue<Action> actionQueue = new Queue<Action>();

        public virtual void Update(float deltaTimeSeconds, int speedFactor)
        {
            double elapsedTimeAdjusted = (double)deltaTimeSeconds * speedFactor;
            while (actionQueue.Count > 0)
            {
                var action = actionQueue.Dequeue();
                action();
            }
            CalculateHydrationPercent(elapsedTimeAdjusted);
            hunger += speedFactor;
            if (hydrationPercent < -100)
            {
                Debug.Log($"{owner.GetType().Name} died of dehydration");
                TransitionTo(new Dead(owner, hydrationPercent, hunger));
            }
            /*if (hunger > owner.CriticalHungerLimit)
            {
                Debug.Log($"{owner.GetType().Name} has starved to death");
                TransitionTo(new Dead(owner, thirst, hunger));
            }*/
        }

        protected State(Animal owner, double hydrationPercent, float hunger)
        {
            this.owner = owner;
            this.hydrationPercent = hydrationPercent;
            this.hunger = hunger;

        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnExit()
        {
        }

        public virtual void OnInterrupted()
        {
        }

        protected void TransitionTo(State newState)
        {
            if (transitioned)
            {
                return;
            }
            owner.SetState(newState);
            transitioned = true;
        }

        protected void TransitionToNextUpdate(State state)
        {
            NextUpdate(() => TransitionTo(state));
        }

        protected void AllowSearchingWater()
        {
            if (hydrationPercent < owner.Metadata.ThirstyPercent)
            {
                Debug.Log($"{owner.GetType().Name} is thirsty");
                TransitionTo(new SearchingWater(owner, hydrationPercent, hunger));
            }
        }

        protected void AllowSearchingFood()
        {
            /*if (hunger > owner.HungerLimit)
            {
                Debug.Log($"{owner.GetType().Name} is hungry");
                TransitionTo(owner.HandleFoodFinding());
            }*/
        }

        protected virtual void NextUpdate(Action action)
        {
            actionQueue.Enqueue(action);
        }

        private void CalculateHydrationPercent(double elapsedTimeAdjusted)
        {
            if (hydrationPercent > 0)
            {
                // simply thirsty
                hydrationPercent -= elapsedTimeAdjusted / (owner.Metadata.TimeTillThirsty * 60);
                if (hydrationPercent < 0)
                {
                    hydrationPercent = 0;
                }
            }
            else
            {
                // DEHYDRATING!!!
                hydrationPercent -= elapsedTimeAdjusted / (owner.Metadata.TimeTillDehydration * 60);
            }
        }
    }
}
