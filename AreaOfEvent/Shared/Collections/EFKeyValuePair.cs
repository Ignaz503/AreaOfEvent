using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaOfEvent.Shared.Collections
{
    public class EFKeyValuePair<TKey, TValue>
    {
        [Key]
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public static implicit operator EFKeyValuePair<TKey, TValue>( KeyValuePair<TKey, TValue> pair )
            => new() { Key = pair.Key, Value = pair.Value };

        public static implicit operator KeyValuePair<TKey, TValue>( EFKeyValuePair<TKey, TValue> pair )
            => new( key: pair.Key, value: pair.Value );

    }
}
