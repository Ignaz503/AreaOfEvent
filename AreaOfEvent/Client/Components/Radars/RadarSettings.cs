using AreaOfEvent.Client.Components.Radars.Behaviours;
using AreaOfEvent.Client.Components.Rendering;
using AreaOfEvent.Client.Components.Rendering.Shapes;
using AreaOfEvent.Shared.Mathematics;
using AreaOfEvent.Shared.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AreaOfEvent.Client.Components.Radars
{
    public class RadarSettings
    {
        public event Action<RadarSettings> OnSettingsChanged;


        private double maxRadiusSize = 0.4;
        private double minRadiusSize = 0.04;

        public double MaxRadiusSize
        {
            get => maxRadiusSize;
            set
            {
                UpdateProperty( ref maxRadiusSize, value );
            }
        }

        private double MinRadiusSize
        {
            get => minRadiusSize;
            set
            {
                UpdateProperty( ref minRadiusSize, value );
            }
        }

        public double GetMaxRadius( double width, double height )
        {
            double minSide = Math.Min(width,height);

            double maxRadius = minSide * MaxRadiusSize;
            return maxRadius;
        }

        public double GetMinRadius( double width, double height )
        {
            double minSide = Math.Min(width,height);

            return minSide * MinRadiusSize;
        }

        public (double min, double max) GetMinMaxRadius( double width, double height )
        {
            return (GetMinRadius( width, height ), GetMaxRadius( width, height ));
        }

        void UpdateProperty<T>( ref T backgroundStore, T newVal )
        {
            if (EqualityComparer<T>.Default.Equals( newVal, backgroundStore ))
                return;
            backgroundStore = newVal;
            OnSettingsChanged?.Invoke( this );
        }

    }

    public class RadarBackground : SceneObject
    {
        public RadarBackground( RadarSettings s, ReadonlyRef<double> minDistance, ReadonlyRef<double> maxDistance, int circles, string strokeStyle, Scene scene ) : base( scene )
        {
            AddComponent( new CenterInScene( this ) );

            AddComponent( new RadarBackgroundCross( s, this ) { StrokeStyle = strokeStyle } );


            double divisor =(circles-1 > 0 ? circles -1 : 1);
            for (int i = 0; i < circles; i++)
            {
                var t = (double)i / divisor;

                var maxRad = s.GetMaxRadius( scene.Width, scene.Height );

                var rad = Maths.Lerp( s.GetMinRadius( scene.Width, scene.Height ), s.GetMaxRadius( scene.Width, scene.Height ), t );


                var wireCircle = new WireCircle(strokeStyle,rad,false);

                AddComponent( new RadarBackgroundCircle( s, wireCircle, this ) { T = t } );

                var normalizedRad = rad / maxRad;
                Vector2 offset = new(2, -rad - 2); //2 pixel offset to circle

                AddComponent( new DistanceIndicator( s, this )
                {
                    MinDistance = minDistance,
                    MaxDistance = maxDistance,
                    T = normalizedRad,
                    Offset = offset,
                    FontInfo = "smaller consolas",
                    DrawStyle = strokeStyle
                } );
            }

            //todo add the lines and n,s e, w texts
        }
    }

}
