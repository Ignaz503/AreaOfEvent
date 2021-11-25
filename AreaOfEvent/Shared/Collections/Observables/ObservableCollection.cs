using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaOfEvent.Shared.Collections.Observables
{
    public class ObservableCollection<T> : IEnumerable<T>
    {
        public event Action<object, CollectionChangedEventArgs<T>> OnCollectionChanged;

        List<T> data;

        IEqualityComparer<T> equalityComparer;

        public IEqualityComparer<T> EqualityComparer
        {
            get => equalityComparer;
            set
            {
                if (value == null)
                {
                    equalityComparer = EqualityComparer<T>.Default;
                    return;
                }
                equalityComparer = value;
            }
        }

        public int Count => data.Count;


        public ObservableCollection()
        {
            data = new();
            EqualityComparer = null;
        }

        public ObservableCollection( IEqualityComparer<T> equalityComparer ) : this()
        {
            EqualityComparer = equalityComparer;
        }

        public ObservableCollection( IEnumerable<T> src )
        {
            data = new( src );
            EqualityComparer = null;
        }

        public ObservableCollection( IEnumerable<T> src, IEqualityComparer<T> equalityComparer )
        {
            data = new( src );
            this.EqualityComparer = equalityComparer;
        }

        public T this[int idx]
        {
            get => data[idx];
            set
            {
                Insert( idx, value );
            }
        }

        public void Add( T item )
        {
            data.Add( item );
            NotifyCollectionChange( new AddItemCollectionChangedEventArgs<T> { ChangeIdx = data.Count - 1 } );
        }

        public void AddRange( ICollection<T> items )
        {
            if (items.Count == 0)
                return;

            int startIdx = data.Count;
            int endIdx = startIdx + (items.Count -1);
            data.AddRange( items );
            NotifyCollectionChange( new AddRangeCollectionChangedEventArgs<T> { StartChangeIdx = startIdx, EndChangeIdx = endIdx } );
        }

        public void Insert( int idx, T item )
        {
            if (equalityComparer.Equals( item, data[idx] ))
                return;
            var old = data[idx];
            data[idx] = item;
            NotifyCollectionChange( new InsertAtCollectionChangedEventArgs<T> { ChangeIdx = idx, OldItem = old, NewItem = item } );
        }

        public bool Remove( T item )
        {
            int idx = data.FindIndex(t => equalityComparer.Equals(t,item));
            if (idx == -1)
                return false;
            data.RemoveAt( idx );
            NotifyCollectionChange( new RemovedItemCollectionChangedEventArgs<T> { ChangeIdx = idx, ItemRemoved = item } );
            return true;
        }

        public void RemoveAt( int idx )
        {
            if (idx < 0 && idx >= data.Count)
                return;

            var item = data[idx];
            data.RemoveAt( idx );
            NotifyCollectionChange( new RemovedItemCollectionChangedEventArgs<T> { ChangeIdx = idx, ItemRemoved = item } );
        }

        public void RemoveRange( int startIdx, int count )
        {
            if (count == 0)
                return;
            if (count == 1)
            {
                RemoveAt( startIdx );
                return;
            }
            data.RemoveRange( startIdx, count );
            NotifyCollectionChange( new RemovedRangeCollectionChangedEventArgs<T> { StartIdx = startIdx, Count = count } );
        }

        public bool Contains( T item )
            => data.Contains( item );

        public void Clear()
        {
            data.Clear();
            NotifyCollectionChange( new ClearedCollectionChangedEventArgs<T>() );
        }

        void NotifyCollectionChange( CollectionChangedEventArgs<T> args )
        {
            OnCollectionChanged?.Invoke( this, args );
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var elem in data)
            {
                yield return elem;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public abstract record CollectionChangedEventArgs<T>
    { }

    public record ClearedCollectionChangedEventArgs<T> : CollectionChangedEventArgs<T> { }

    public abstract record SingleItemCollectionChangedEventArgs<T> : CollectionChangedEventArgs<T>
    {
        public int ChangeIdx { get; init; }
    }

    public record RemovedRangeCollectionChangedEventArgs<T> : CollectionChangedEventArgs<T>
    {
        public int StartIdx { get; init; }
        public int Count { get; init; }

    }

    public abstract record RangeCollectionChangedEventArgs<T> : CollectionChangedEventArgs<T>
    {
        public int StartChangeIdx { get; init; }
        public int EndChangeIdx { get; init; }
    }

    public record AddItemCollectionChangedEventArgs<T> : SingleItemCollectionChangedEventArgs<T>
    { }

    public record AddRangeCollectionChangedEventArgs<T> : RangeCollectionChangedEventArgs<T>
    { }

    public record InsertAtCollectionChangedEventArgs<T> : SingleItemCollectionChangedEventArgs<T>
    {
        public T OldItem { get; init; }
        public T NewItem { get; init; }
    }

    public record RemovedItemCollectionChangedEventArgs<T> : SingleItemCollectionChangedEventArgs<T>
    {
        public T ItemRemoved { get; init; }

    }
}
