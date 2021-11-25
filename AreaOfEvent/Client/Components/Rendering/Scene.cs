using Microsoft.JSInterop;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AreaOfEvent.Client.Components.Rendering.Components;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;


namespace AreaOfEvent.Client.Components.Rendering
{
    public class Scene : IDisposable
    {
        public event Action OnResize;

        public long Width { get; private set; }
        public long Height { get; private set; }

        public object BackgroundColor { get; set; }

        public Timing FrameTime { get; private set; } = new();
        ConcurrentDictionary<Guid,SceneObject> sceneObjects = new();
        ConcurrentBag<SceneObject> makredForRemoval = new();

        public Scene( long width, long height )
         => (Width, Height) = (width, height);

        public (double x, double y) GetCenter()
            => (this.Width * 0.5, this.Height * 0.5);
        public async Task Update( float totalTime, IJSRuntime jsr, int sceneWidth, int sceneHeight )
        {
            var (oldW, oldH) = (Width, Height);

            (Width, Height) = (sceneWidth, sceneHeight);
            FrameTime.TotalTime = totalTime;

            if (oldW != Width || oldH != Height)
                OnResize?.Invoke();

            UpdateContext ctx = new() { Scene = this, JSRuntime = jsr };

            foreach (var obj in sceneObjects)
                await obj.Value.Update( ctx );
            HandleSceneObjectDestruction();
        }

        public async Task Render( BECanvasComponent canvasComponent, Canvas2DContext canvasCTX, IJSRuntime jsr )
        {
            RenderContext ctx = new(){Scene = this, CanvasComponent = canvasComponent, CanvasCTX = canvasCTX, JSRuntime = jsr };


            await ctx.CanvasCTX.BeginBatchAsync();

            await ctx.CanvasCTX.ClearRectAsync( 0, 0, Width, Height );
            await ctx.CanvasCTX.SetFillStyleAsync( BackgroundColor );
            await ctx.CanvasCTX.FillRectAsync( 0, 0, Width, Height );

            foreach (var obj in sceneObjects)
                await obj.Value.Render( ctx );

            await ctx.CanvasCTX.EndBatchAsync();
        }

        public async Task OnClick( ClickConext ctx )
        {
            foreach (var obj in sceneObjects)
            {
                await obj.Value.Interactions( ctx );
            }
        }

        public SceneObject CreateSceneObject()
        {
            var newObject = new SceneObject(this){ ID = Guid.NewGuid() };
            sceneObjects.TryAdd( newObject.ID, newObject );
            return newObject;
        }

        public SceneObject CreateSceneObject( Func<Guid, SceneObject> factory )
        {
            var newObject = factory( Guid.NewGuid() );
            sceneObjects.TryAdd( newObject.ID, newObject );
            return newObject;
        }

        public bool RemoveSceneObject( SceneObject obj )
        {
            if (makredForRemoval.Contains( obj ))
                return true;


            var removed = sceneObjects.TryRemove(obj.ID, out _);

            if (removed)
                makredForRemoval.Add( obj );
            return removed;

        }

        void HandleSceneObjectDestruction()
        {

            foreach (var obj in makredForRemoval)
                obj.Destroy();
            makredForRemoval.Clear();

        }

        public SceneObject FindObjectByName( string name )
        {

            foreach (var obj in sceneObjects)
            {
                if (obj.Value.Name == name)
                    return obj.Value;
            }
            return default;
        }
        public void RemoveAllObjectsWithComponent<T>() where T : Component
        {
            List<SceneObject> toRemove = new();

            foreach (var obj in sceneObjects)
            {
                if (obj.Value.GetComponent<T>() != null)
                {
                    Console.WriteLine( "Removing a component" );
                    toRemove.Add( obj.Value );
                }
            }

            foreach (var o in toRemove)
            {
                RemoveSceneObject( o );
            }

        }

        public IEnumerable<SceneObject> FindObjectsWithComponent<T>() where T : Component
        {
            foreach (var obj in sceneObjects)
            {
                if (obj.Value.GetComponent<T>() != null)
                    yield return obj.Value;
            }
        }

        public void Dispose()
        {
            foreach (var obj in sceneObjects)
                obj.Value.Destroy();
            foreach (var obj in makredForRemoval)
                obj.Destroy();

            sceneObjects.Clear();
            makredForRemoval.Clear();
        }
    }
}
