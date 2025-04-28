#nullable enable
namespace Safari.Model.Animals.Movement
{

    public class StalkingFinishedEventArgs
    {
        public StalkingResult Result { get; private set; }

        public StalkingFinishedEventArgs(StalkingResult result)
        {
            Result = result;
        }
    }

}
