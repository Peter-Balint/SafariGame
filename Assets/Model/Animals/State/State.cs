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

        public double BreedingCooldown => breedingCooldown;

        public double Age => age;

        protected double hydrationPercent;

        protected double saturationPercent;

        // Unit: days
        protected double age;

        protected Animal owner;

        protected virtual bool DisableThirst => false;

        protected virtual bool DisableHunger => false;

        protected double breedingCooldown = 0;

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
            breedingCooldown -= elapsedTimeAdjusted;
            if (breedingCooldown < 0)
            {
                breedingCooldown = 0;
            }
            CalculateHydrationPercent(elapsedTimeAdjusted);
            CalculateSaturationPercent(elapsedTimeAdjusted);
            if (hydrationPercent < -100)
            {
                Debug.Log($"{owner.GetType().Name} died of dehydration");
                TransitionTo(new Dead(owner, hydrationPercent, saturationPercent, breedingCooldown));
                return;
            }
            if (saturationPercent < -100)
            {
                Debug.Log($"{owner.GetType().Name} has starved to death");
                TransitionTo(new Dead(owner, hydrationPercent, saturationPercent, breedingCooldown));
                return;
            }
            if (age > owner.Metadata.LifeSpan)
            {
                Debug.Log($"{owner.GetType().Name} has died of old age");
                TransitionTo(new Dead(owner, hydrationPercent, saturationPercent, breedingCooldown));
            }
        }

        protected State(Animal owner, double hydrationPercent, double saturationPercent, double breedingCooldown)
        {
            this.owner = owner;
            this.hydrationPercent = hydrationPercent;
            this.saturationPercent = saturationPercent;
            this.breedingCooldown = breedingCooldown;
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

        public virtual bool CanMate()
        {
            if (age < owner.Metadata.MinBreedingAge || age > owner.Metadata.MaxBreedingAge)
            {
                return false;
            }

            if (saturationPercent <= 50 || hydrationPercent <= 50)
            {
                return false;
            }
            return breedingCooldown <= 0;
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
                TransitionTo(new SearchingWater(owner, hydrationPercent, saturationPercent, breedingCooldown));
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

        protected void AllowSearchingMate()
        {
            if (owner.gender == Gender.Female)
            {
                return;
            }

            if (!CanMate())
            {
                return;
            }

            if(UnityEngine.Random.Range(0, 100) >= 40)
            {
                return;
            }

            TransitionTo(new SearchingMate(owner, hydrationPercent, saturationPercent, breedingCooldown));
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
