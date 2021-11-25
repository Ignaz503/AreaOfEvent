using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AreaOfEvent.Client.Components.Rendering.Components;


namespace AreaOfEvent.Client.Components.Rendering
{
    public class SceneObject
    {
        public event Action<SceneObject> OnSceneObjectDestroyed;

        public Guid ID { get; init; }
        public string Name { get; set; }

        public Scene Scene { get; private set; }

        public Transform Transform { get; init; }

        List<Component> components = new();

        public SceneObject( Scene scene )
        {
            this.Scene = scene;
            Transform = new( this );
        }

        public async Task Update( UpdateContext ctx )
        {
            foreach (var component in components)
            {
                if (component.Enabled && component is IUpdateable uc)
                    await uc.Update( ctx );
            }
        }

        public async Task Render( RenderContext ctx )
        {
            foreach (var component in components)
            {
                if (component.Enabled && component is IRenderable rc)
                    await rc.Render( ctx );
            }
        }

        public async Task Interactions( ClickConext ctx )
        {
            foreach (var component in components)
            {
                if (component.Enabled && component is IInteractable ic)
                {
                    Console.WriteLine( $"Comparing click {ctx} to {Transform.Position}" );
                    await ic.CheckHit( ctx );
                }
            }
        }

        public T AddComponent<T>( T component ) where T : Component
        {
            components.Add( component );
            return component;
        }

        public bool Remove<T>( T component ) where T : Component
            => components.Remove( component );

        public T GetComponent<T>() where T : Component
        {
            foreach (var c in components)
                if (c.GetType() == typeof( T ))
                    return c as T;
            return null;
        }

        public void Destroy()
        {
            Scene = null;
            foreach (var c in components)
                c.Destroy();
            OnSceneObjectDestroyed?.Invoke( this );
        }

    }

}
