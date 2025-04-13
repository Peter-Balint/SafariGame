namespace Safari.Model.Animals.State
{
    public class Dead : State
    {
        public Dead(Animal owner, float thirst, float hunger) : base(owner, thirst, hunger)
        {
        }
    }
}