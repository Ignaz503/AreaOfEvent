using AreaOfEvent.Client.Components.Rendering;
using AreaOfEvent.Client.Components.Rendering.Shapes;
using AreaOfEvent.Shared.Mathematics;
using System;

namespace AreaOfEvent.Client.Components.Radars.Behaviours
{
    public class RadarBackgroundCircle : ShapeRenderer
    {

        public RadarSettings Settings { get; private set; }

        public double T { get; set; }

        public RadarBackgroundCircle( RadarSettings settings, IShape shape, SceneObject containingObject ) : base( shape, containingObject )
        {
            this.Settings = settings ?? throw new ArgumentNullException( nameof( settings ) );

            Settings.OnSettingsChanged += OnSettingsChanged;
            containingObject.Scene.OnResize += UpdateShape;
        }

        void OnSettingsChanged( RadarSettings s )
        {
            UpdateShape();
        }

        private void UpdateShape()
        {
            var (width, height) = (SceneObject.Scene.Width, SceneObject.Scene.Height);
            if (Shape is Arc arc)
            {
                arc.Radius = Maths.Lerp( Settings.GetMinRadius( width, height ), Settings.GetMaxRadius( width, height ), T );
            }
        }
        public override void Destroy()
        {
            Settings.OnSettingsChanged -= OnSettingsChanged;
            SceneObject.Scene.OnResize -= UpdateShape;

            base.Destroy();
        }

    }

}
