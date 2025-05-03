using Safari.Model.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Animals.State
{
    public class WaitingForOthers : State
    {
        private MovementCommand key;

        public WaitingForOthers(Animal owner, double hydrationPercent, double saturationPercent, MovementCommand key) : base(owner, hydrationPercent, saturationPercent)
        {
            this.key = key;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
        }
        
        public override void Update(float deltaTime, int speedFactor)
        {
            base.Update(deltaTime, speedFactor);
            AllowSearchingWater();
            AllowSearchingFood();
            bool allFinished = true;
            foreach (var animal in owner.Group.Animals)
            {
                // még nem kezdett el másik wanderinget
                bool executingPrevWandering = animal.State is Wandering w && w.Key == key;
                if (animal.State is not WaitingForOthers && executingPrevWandering )
                {
                    allFinished = false;
                }
            }
            if (allFinished)
            {
               TransitionTo(new Wandering(owner, hydrationPercent, saturationPercent));
            }
        }
    }
}
