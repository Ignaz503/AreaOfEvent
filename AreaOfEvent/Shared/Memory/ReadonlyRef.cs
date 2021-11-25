using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaOfEvent.Shared.Memory
{
    public sealed class ReadonlyRef<T>
    {
        Func<T> getter;

        public ReadonlyRef( Func<T> getter )
        {
            this.getter = getter ?? throw new ArgumentNullException( nameof( getter ) );
        }

        public T Value { get => getter(); }

        public static implicit operator T( ReadonlyRef<T> @ref )
            => @ref.Value;

    }
}
