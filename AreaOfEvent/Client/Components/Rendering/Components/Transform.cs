using AreaOfEvent.Shared.Mathematics;

namespace AreaOfEvent.Client.Components.Rendering.Components
{
    public class Transform
    {
        public SceneObject SceneObject { get; private set; }

        public Vector2 Position { get; set; }
        public float RotationRadian { get; set; }

        public Transform( SceneObject obj )
        {
            this.SceneObject = obj;
            SceneObject.OnSceneObjectDestroyed += Destroy;
        }

        void Destroy( SceneObject obj )
        {
            this.SceneObject.OnSceneObjectDestroyed -= Destroy;
            this.SceneObject = null;
        }

    }

}
