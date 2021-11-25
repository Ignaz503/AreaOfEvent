using AreaOfEvent.Client.Components.Rendering;
using AreaOfEvent.Client.Components.Rendering.Components;
using AreaOfEvent.Shared.Mathematics;
using AreaOfEvent.Shared.Memory;
using System;
using System.Threading.Tasks;

namespace AreaOfEvent.Client.Components.Radars.Behaviours
{
    public class DistanceIndicator : TextRenderer, IUpdateable
    {
        public Vector2 Offset { get; set; }

        public double T { get; set; }
        double maxDistLatUpdate;
        public ReadonlyRef<double> MaxDistance { get; set; }
        public ReadonlyRef<double> MinDistance { get; set; }

        protected override Vector2 DrawPoistion => base.DrawPoistion + Offset;

        public float RotationRadian { get; set; }
        protected override float DrawRotationRadian { get => RotationRadian; }

        RadarSettings settings;

        public DistanceIndicator( RadarSettings settings, SceneObject containingObject ) : base( containingObject )
        {
            maxDistLatUpdate = 0d;
            this.settings = settings;
            SceneObject.Scene.OnResize += UpdateOffset;
            settings.OnSettingsChanged += OnSettingsChanged;
        }

        public Task Update( UpdateContext ctx )
        {
            //check if max distance differs from last update
            if (!Maths.AlmostEquals( maxDistLatUpdate, MaxDistance ))
            {
                double min = MinDistance;
                double max = MaxDistance;

                if (Maths.AlmostEquals( min, max ))
                {
                    min = Math.Min( 1, max - 1 );
                }

                maxDistLatUpdate = MaxDistance;
                this.Text = $"{Math.Round( Maths.Lerp( min, max, T ) / 1000 )}km";
            }
            return Task.CompletedTask;
        }

        void OnSettingsChanged( RadarSettings s )
        {
            UpdateOffset();
        }

        void UpdateOffset()
        {
            var rad = settings.GetMaxRadius(SceneObject.Scene.Width, SceneObject.Scene.Height) * T;
            Offset = new( 2, -rad - 2 ); //2 pixel offset to circle
        }

        public override void Destroy()
        {
            SceneObject.Scene.OnResize -= UpdateOffset;
            settings.OnSettingsChanged -= OnSettingsChanged;
            base.Destroy();
        }
    }
}
