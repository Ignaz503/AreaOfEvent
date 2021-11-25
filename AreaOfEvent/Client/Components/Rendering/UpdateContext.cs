using Microsoft.JSInterop;


namespace AreaOfEvent.Client.Components.Rendering
{
    public struct UpdateContext
    {
        public Scene Scene { get; init; }

        public Timing FrameTime => Scene.FrameTime;

        public IJSRuntime JSRuntime { get; init; }
    }

}
