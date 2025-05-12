namespace Safari.Model.Animals.State
{
    public class Dead : State
    {
        public Dead(Animal owner, double hydrationPercent, double saturationPercent, double breedingCooldown) : base(owner, hydrationPercent, saturationPercent, breedingCooldown)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            owner.Movement.AbortMovement();
        }
    }
}