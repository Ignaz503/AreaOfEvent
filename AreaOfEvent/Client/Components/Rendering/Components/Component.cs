using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AreaOfEvent.Client.Components.Rendering.Components
{
    public abstract class Component
    {
        public event Action<Component> OnComponentDestroyed;

        public SceneObject SceneObject { get; private set; }

        public Transform Transform => SceneObject.Transform;

        public Component( SceneObject containingObject )
        {
            Enabled = true;
            SceneObject = containingObject;
        }

        public bool Enabled { get; set; }

        public virtual void Destroy()
        {
            SceneObject = null;
            OnComponentDestroyed?.Invoke( this );
        }
    }

}
