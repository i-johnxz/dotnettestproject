using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TopologicalSorting
{
    public static class TopologicalSort
    {

        private static Func<T, IEnumerable<T>> RemapDependencies<T, TKey>(IEnumerable<T> source,
            Func<T, IEnumerable<TKey>> getDependencies, Func<T, TKey> getKey)
        {
            var map = source.ToDictionary(getKey);
            return item =>
            {
                var dependencies = getDependencies(item);
                return dependencies?.Select(key => map[key]);
            };
        }

        public static IList<T> Sort<T, TKey>(IEnumerable<T> source, Func<T, IEnumerable<TKey>> getDependencies,
            Func<T, TKey> getKey)
        {
            return Sort<T>(source, RemapDependencies(source, getDependencies, getKey));
        }

        public static IList<T> Sort<T, TKey>(IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies,
            Func<T, TKey> getkey)
        {
            return Sort<T>(source, getDependencies, new GenericEqualityComparer<T, TKey>(getkey));
        }

        public static IList<T> Sort<T>(IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies,
            IEqualityComparer<T> comparer = null)
        {
            var sorted = new List<T>();
            var visited = new Dictionary<T, bool>(comparer);
            foreach (var item in source)
            {
                Visit(item, getDependencies, sorted, visited);
            }

            return sorted;
        }

        public static IList<ICollection<T>> Group<T>(IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies,
            IEqualityComparer<T> comparer = null)
        {
            var sorted = new List<ICollection<T>>();
            var visited = new Dictionary<T, int>(comparer);

            foreach (var item in source)
            {
                Visit(item, getDependencies, sorted, visited);
            }

            return sorted;
        }

        public static int Visit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<ICollection<T>> sorted,
            Dictionary<T, int> visited)
        {
            const int inProcess = -1;
            var alreadyVisited = visited.TryGetValue(item, out var level);

            if (alreadyVisited)
            {
                if (level == inProcess)
                {
                    throw new ArgumentException("Cyclic dependency found.");
                }
            }
            else
            {
                visited[item] = (level = inProcess);

                var dependencies = getDependencies(item);
                if (dependencies != null)
                {
                    foreach (var dependency in dependencies)
                    {
                        var depLevel = Visit(dependency, getDependencies, sorted, visited);
                        level = Math.Max(level, depLevel);
                    }
                }

                visited[item] = ++level;
                while (sorted.Count <= level)
                {
                    sorted.Add(new Collection<T>());
                }
                sorted[level].Add(item);
            }

            return level;
        }

        public static void Visit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted,
            Dictionary<T, bool> visited)
        {
            var alreadyVisited = visited.TryGetValue(item, out var inProcess);

            if (alreadyVisited)
            {
                if (inProcess)
                {
                    throw new ArgumentException("Cyclic dependency found");
                }
            }
            else
            {
                visited[item] = true;

                var dependencies = getDependencies(item);
                if (dependencies != null)
                {
                    foreach (var dependency in dependencies)
                    {
                        Visit(dependency, getDependencies, sorted, visited);
                    }
                }

                visited[item] = false;
                sorted.Add(item);
            }
        }
    }
}
