using AreaOfEvent.Client.Components.Rendering;
using AreaOfEvent.Client.Components.Rendering.Components;
using AreaOfEvent.Client.Components.Rendering.Shapes;
using AreaOfEvent.Shared.Mathematics;
using System;
using System.Threading.Tasks;

namespace AreaOfEvent.Client.Components.Radars.Behaviours
{
    public class RadarLineRotator : Component, IRadarElement, IUpdateable
    {
        Line toRotate;

        public double DegreePerSecond { get; set; }
        public RadarSettings Settings { get; set; }

        double currentAngle = 0f;

        public RadarLineRotator( Line toRotate, RadarSettings settings, SceneObject containingObject ) : base( containingObject )
        {
            this.toRotate = toRotate;
            Settings = settings;
        }

        public Task Update( UpdateContext ctx )
        {
            currentAngle += DegreePerSecond * ctx.FrameTime.DeltaTime;

            currentAngle = Maths.ClampAngleDegree( currentAngle );

            double maxRadius = Settings.GetMaxRadius( ctx.Scene.Width, ctx.Scene.Height);
            var to = currentAngle * Maths.DegToRad;
            double targetToX = Math.Cos(to) * (maxRadius) + SceneObject.Transform.Position.X;
            double targetToY = Math.Sin(to) *(maxRadius) + SceneObject.Transform.Position.Y;


            toRotate.To = new() { X = targetToX, Y = targetToY };

            return Task.CompletedTask;
        }
    }
}
