using Safari.Model.Animals.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safari.Model.Animals.State
{
    public class Mating : State
    {
        private Animal mate;

        private ApproachMovementCommand command;

        public Mating(Animal owner, double hydrationPercent, double saturationPercent, double breedingCooldown, Animal mate ) : base(owner, hydrationPercent, saturationPercent, breedingCooldown)
        {
            this.mate = mate;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            command = new ApproachMovementCommand(mate.Movement);
            command.Finished += OnMovementFinished;
            mate.StateChanged += OnMateStateChanged;
            owner.Movement.ExecuteMovement(command);
        }

        private void OnMateStateChanged(object sender, EventArgs e)
        {
            if (mate.State is Mating)
            {
                return;
            }
            mate.StateChanged -= OnMateStateChanged;
            OnMatingFailed();
        }

        private void OnMatingFailed()
        {
            command.Finished -= OnMovementFinished;
            double breedingCooldown;
            if (owner.gender == Gender.Female)
            {
                breedingCooldown = owner.Metadata.FemaleBreedingCooldown;
            }
            else
            {
                breedingCooldown = owner.Metadata.MaleBreedingCooldown;
            }
            TransitionToNextUpdate(new Resting(owner, hydrationPercent, saturationPercent, breedingCooldown));
        }

        private void OnMovementFinished(object sender, EventArgs e)
        {
            mate.StateChanged -= OnMateStateChanged;
            double breedingCooldown;
            if (owner.gender == Gender.Female && mate.State is Mating)
            {
                breedingCooldown = owner.Metadata.FemaleBreedingCooldown;
                owner.AnimalCollection.AddAnimal(owner.OffspringFactory());
            }
            else
            {
                breedingCooldown = owner.Metadata.MaleBreedingCooldown;
            }
            TransitionToNextUpdate(new Resting(owner, hydrationPercent, saturationPercent, breedingCooldown));
        }

        public override void OnExit()
        {
            base.OnExit();
            command?.Cancel();
        }
    }
}
