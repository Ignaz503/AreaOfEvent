using Microsoft.JSInterop;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;


namespace AreaOfEvent.Client.Components.Rendering
{
    public struct RenderContext
    {
        public Scene Scene { get; init; }
        public BECanvasComponent CanvasComponent { get; init; }
        public Canvas2DContext CanvasCTX { get; init; }

        public IJSRuntime JSRuntime { get; init; }

    }

}
