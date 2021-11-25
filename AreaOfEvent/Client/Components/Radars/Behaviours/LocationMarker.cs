using AreaOfEvent.Client.Components.Rendering;
using AreaOfEvent.Client.Components.Rendering.Components;
using AreaOfEvent.Client.Components.Rendering.Shapes;
using AreaOfEvent.Shared.GeoLocation;
using AreaOfEvent.Shared.Mathematics;
using AreaOfEvent.Shared.Memory;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AreaOfEvent.Client.Components.Radars.Behaviours
{
    public class LocationMarker<T> : ShapeRenderer, IUpdateable, IRadarElement, IInteractable<T> where T : IGeoLocateable
    {
        const double MaxTValue = 1.2;

        public T Data { get; private set; }

        public EventCallback<T> OnHit { get; set; }
        public ReadonlyRef<double> MaxDistance { get; set; }

        public RadarSettings Settings { get; set; }

        UserLocationTracker userLocation;

        public LocationMarker( T data, EventCallback<T> callback, UserLocationTracker userLocation, ReadonlyRef<double> maxDistance, RadarSettings settings, IShape shape, SceneObject containingObject ) : base( shape, containingObject )
        {
            this.Shape = shape;
            this.Settings = settings;
            this.Data = data;
            this.OnHit = callback;
            this.MaxDistance = maxDistance;
            this.userLocation = userLocation;
        }

        public async Task CheckHit( ClickConext ctx )
        {
            if (Shape != null)
            {
                if (Shape.Contains( Transform.Position, new() { X = ctx.MouseX, Y = ctx.MouseY } ))
                {
                    await OnHit.InvokeAsync( Data );
                }
            }
        }

        public bool DataEquals( T obj )
        {
            return EqualityComparer<T>.Default.Equals( obj, Data );
        }


        void FigureOutPosition( Scene scene )
        {
            if (userLocation == null)
                return;
            var currentDistance = Location.Maths.Distance(userLocation.Location,Data.GeoLocation);
            var bearinginDeg = Location.Maths.BearingInDegrees(userLocation.Location,Data.GeoLocation) + -90d;

            var t = Maths.AlmostEquals(MaxDistance,0) ? 0 : currentDistance / MaxDistance;

            t = Math.Clamp( t, 0, MaxTValue );

            var (centerX, centerY) = scene.GetCenter();

            double maxRad = Settings.GetMaxRadius(scene.Width,scene.Height);

            SceneObject.Transform.Position = new Vector2(
                x: Math.Cos( bearinginDeg * Maths.DegToRad ) * Maths.Lerp( 0, maxRad, t ) + centerX,
                y: Math.Sin( bearinginDeg * Maths.DegToRad ) * Maths.Lerp( 0, maxRad, t ) + centerY );
        }

        //update position because maybe we are actively tracking 
        public Task Update( UpdateContext ctx )
        {
            FigureOutPosition( ctx.Scene );
            return Task.CompletedTask;
        }

        public override void Destroy()
        {
            userLocation = null;
            base.Destroy();
        }
    }
}
