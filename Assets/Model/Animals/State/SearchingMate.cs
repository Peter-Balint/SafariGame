using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Animals.State
{
    public class SearchingMate : State
    {
        private Func<double, double, double, State> nextState;

        public SearchingMate(Animal owner, double hydrationPercent, double saturationPercent, double breedingCooldown, Func<double, double, double, State> nextState) : base(owner, hydrationPercent, saturationPercent, breedingCooldown)
        {
            this.nextState = nextState;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Animal? mate = null;
            foreach (var animal in owner.Group.Animals)
            {
                if (animal.gender == Gender.Female && animal.State.CanMate())
                {
                    mate = animal;
                    break;
                }
            }
            if (mate == null)
            {
                UnityEngine.Debug.Log("No mate found");
                var cooldown = owner.Metadata.MaleBreedingCooldown / 4;
                TransitionTo(nextState(hydrationPercent, saturationPercent, cooldown));
                return;
            }
            mate.OnApproachedByMate(owner);
            TransitionTo(new Mating(owner, hydrationPercent, saturationPercent, breedingCooldown, mate));
        }
    }
}
