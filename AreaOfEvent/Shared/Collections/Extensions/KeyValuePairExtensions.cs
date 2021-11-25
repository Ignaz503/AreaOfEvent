using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaOfEvent.Shared.Collections.Extensions
{
    public static class KeyValuePairExtensions
    {
        public static IEnumerable<EFKeyValuePair<TKey, TValue>> ConvertToEF<TKey, TValue>( this IEnumerable<KeyValuePair<TKey, TValue>> src )
        {
            foreach (var p in src)
                yield return p;
        }
        public static IEnumerable<KeyValuePair<TKey, TValue>> ConvertFromEF<TKey, TValue>( this IEnumerable<EFKeyValuePair<TKey, TValue>> src )
        {
            foreach (var p in src)
                yield return p;
        }

    }
}
