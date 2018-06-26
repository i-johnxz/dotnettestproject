using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopologicalSorting
{
    public static class Extensions
    {
        class DummyEnumerable<T>:  IEnumerable<T>
        {
            private readonly Func<IEnumerator<T>> getEnumerator;

            public DummyEnumerable(Func<IEnumerator<T>> enumerator)
            {
                getEnumerator = enumerator;
            }


            public IEnumerator<T> GetEnumerator()
            {
                return getEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public static IEnumerable<TItem> TopoSort<TItem, TKey>(this IEnumerable<TItem> source, Func<TItem, TKey> getKey,
            Func<TItem, IEnumerable<TKey>> getDependencies)
        {
            var enumerator = new TopoSortEnumerator<TItem, TKey>(source, getKey, getDependencies);
            return new DummyEnumerable<TItem>(() => enumerator);
        }
    }
}
