namespace Safari.Model.Animals.State
{
    public class Dead : State
    {
        public Dead(Animal owner, double hydrationPercent, float hunger) : base(owner, hydrationPercent, hunger)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            owner.Movement.AbortMovement();
        }
    }
}