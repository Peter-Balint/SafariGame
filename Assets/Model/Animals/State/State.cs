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
        protected int thirst;

        protected float hunger;

        protected Animal owner;

        private bool transitioned = false;

        public virtual void Update(float deltaTime)
        {
            thirst++;
            hunger++;
            if (thirst > owner.CriticalThirstLimit)
            {
                Debug.Log($"{owner.GetType().Name} died of dehydration");
                TransitionTo(new Dead(owner, thirst));
            }
        }

        protected State(Animal owner, int thirst, float hunger)
        {
            this.owner = owner;
            this.thirst = thirst;
            this.hunger = hunger;
            if (hunger > owner.CriticalHungerLimit)
            {
                Debug.Log($"{owner.GetType().Name} has starved to death");
            }
        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnExit()
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

        protected void AllowSearchingWater()
        {
            if (thirst > owner.ThirstLimit)
            {
                Debug.Log($"{owner.GetType().Name} is thirsty");
                TransitionTo(new SearchingWater(owner, thirst, hunger));
            }
        }

        protected void AllowSearchingFood()
        {
            if (hunger > owner.HungerLimit)
            {
                Debug.Log($"{owner.GetType().Name} is hungry");
                TransitionTo(owner.HandleFoodFinding());
            }
        }
    }
}
