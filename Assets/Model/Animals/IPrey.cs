namespace Safari.Model.Animals.Movement
{
    public interface IPrey
    {
        void OnChased(Chaser chaser);

        void Kill();
        void OnEscaped();
    }
}