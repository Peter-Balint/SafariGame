namespace Safari.Model.Animals.State
{
    public class Dead : State
    {
        HANDLE MOVEMENT CANCELLATION PROPERLY
        public Dead(Animal owner, float thirst, float hunger) : base(owner, thirst, hunger)
        {
        }
    }
}