namespace Safari.Model.Animals.State
{
    public class Dead : State
    {
        public Dead(Animal owner, double hydrationPercent, double saturationPercent) : base(owner, hydrationPercent, saturationPercent)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            owner.Movement.AbortMovement();
        }
    }
}