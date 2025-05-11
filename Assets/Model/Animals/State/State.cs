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

        public double SaturationPercent => saturationPercent;

        public double Age => age;

        protected double hydrationPercent;

        protected double saturationPercent;

        // Unit: days
        protected double age;

        protected Animal owner;

        protected virtual bool DisableThirst => false;

        protected virtual bool DisableHunger => false;

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
            age += elapsedTimeAdjusted / (60 * 60 * 24);
            CalculateHydrationPercent(elapsedTimeAdjusted);
            CalculateSaturationPercent(elapsedTimeAdjusted);
            if (hydrationPercent < -100)
            {
                Debug.Log($"{owner.GetType().Name} died of dehydration");
                TransitionTo(new Dead(owner, hydrationPercent, saturationPercent));
                return;
            }
            if (saturationPercent < -100)
            {
                Debug.Log($"{owner.GetType().Name} has starved to death");
                TransitionTo(new Dead(owner, hydrationPercent, saturationPercent));
                return;
            }
            if (age > owner.Metadata.LifeSpan)
            {
                Debug.Log($"{owner.GetType().Name} has died of old age");
                TransitionTo(new Dead(owner, hydrationPercent, saturationPercent));
            }
        }

        protected State(Animal owner, double hydrationPercent, double saturationPercent)
        {
            this.owner = owner;
            this.hydrationPercent = hydrationPercent;
            this.saturationPercent = saturationPercent;

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
                TransitionTo(new SearchingWater(owner, hydrationPercent, saturationPercent));
            }
        }

        protected void AllowSearchingFood()
        {
            if (saturationPercent < owner.Metadata.HungryPercent)
            {
                Debug.Log($"{owner.GetType().Name} is hungry");
                TransitionTo(owner.HandleFoodFinding());
            }
        }

        protected virtual void NextUpdate(Action action)
        {
            actionQueue.Enqueue(action);
        }

        private void CalculateHydrationPercent(double elapsedTimeAdjusted)
        {
            if (DisableThirst)
            {
                return;
            }
            if (hydrationPercent > 0)
            {
                hydrationPercent -= (elapsedTimeAdjusted / (owner.Metadata.TimeTillThirsty * 60)) * 100;
                if (hydrationPercent < 0)
                {
                    hydrationPercent = 0;
                }
            }
            else
            {
                hydrationPercent -= (elapsedTimeAdjusted / (owner.Metadata.TimeTillDehydration * 60)) * 100;
            }
        }

        private void CalculateSaturationPercent(double elapsedTimeAdjusted)
        {
            if (DisableHunger)
            {
                return;
            }
            if (saturationPercent > 0)
            {
                double ageFactor = (age / owner.Metadata.LifeSpan) * (owner.Metadata.TimeTillHungry / 4);
                saturationPercent -= (elapsedTimeAdjusted / ((owner.Metadata.TimeTillHungry - ageFactor) * 60)) * 100;
                if (saturationPercent < 0)
                {
                    saturationPercent = 0;
                }
            }
            else
            {
                double ageFactor = (age / owner.Metadata.LifeSpan) * (owner.Metadata.TimeTillStarvation / 4);
                saturationPercent -= (elapsedTimeAdjusted / ((owner.Metadata.TimeTillStarvation - ageFactor) * 60)) * 100;
            }
        }
    }
}
