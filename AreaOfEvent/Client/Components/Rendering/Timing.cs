namespace AreaOfEvent.Client.Components.Rendering
{
    public class Timing
    {
        float totalTime = 0;

        public float TotalTime
        {
            get => totalTime;
            set
            {
                DeltaTime = (value - totalTime) / 1000f;
                totalTime = value;
            }
        }

        public float DeltaTime { get; private set; }
    }

}
