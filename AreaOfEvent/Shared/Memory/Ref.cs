using System;

namespace AreaOfEvent.Shared.Memory
{
    public sealed class Ref<T>
    {
        Func<T> getter;
        Action<T> setter;

        public Ref( Func<T> getter, Action<T> setter )
        {
            this.getter = getter ?? throw new ArgumentNullException( nameof( getter ) );
            this.setter = setter ?? throw new ArgumentNullException( nameof( setter ) );
        }

        public T Value { get => getter(); set => setter( value ); }

        public static implicit operator T( Ref<T> @ref )
            => @ref.Value;
    }
}
