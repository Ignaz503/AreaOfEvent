using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaOfEvent.Shared.Mapping
{
    public interface IMapper<T, K>
    {
        K Map( T value );

    }

    public static class IMapperExtensions
    {
        public static TCol Map<TCol, G, J>( this IMapper<G, J> mapper, IEnumerable<G> source, TCol col )
            where TCol : ICollection<J>
        {
            foreach (var srcElem in source)
                col.Add( mapper.Map( srcElem ) );
            return col;
        }

        public static TCol Map<TCol, G, J>( this IMapper<G, J> mapper, IEnumerable<G> source )
            where TCol : ICollection<J>, new()
        {
            TCol col = new();
            foreach (var srcElem in source)
                col.Add( mapper.Map( srcElem ) );
            return col;
        }
    }
}
