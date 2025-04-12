namespace Safari.Model.Animals.State
{
    public class Dead : State
    {
        public Dead(Animal owner, int thirst) : base(owner, thirst)
        {
        }
    }
}