using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopologicalSorting
{
    public class WaitList<TItem, TKey>
    {
        class Node<T>
        {
            private int dependencyCount;

            public T Item { get; set; }

            public Node(T item, int dependencyCount)
            {
                this.dependencyCount = dependencyCount;
                Item = item;
            }

            public bool DecreaseDependencyCount()
            {
                dependencyCount--;
                return (dependencyCount == 0);
            }

        }


        private readonly Dictionary<TKey, List<Node<TItem>>> dependencies = new Dictionary<TKey, List<Node<TItem>>>();

        public void Add(TItem item, ICollection<TKey> pendingDependencies)
        {
            var node = new Node<TItem>(item, pendingDependencies.Count);

            foreach (var dependency in pendingDependencies)
            {
                Add(dependency, node);
            }
        }

        public IEnumerable<TItem> Remove(TKey key)
        {
            var found = dependencies.TryGetValue(key, out var nodeList);

            if (found)
            {
                dependencies.Remove(key);
                return nodeList.Where(x => x.DecreaseDependencyCount()).Select(x => x.Item);
            }

            return null;
        }

        private void Add(TKey key, Node<TItem> node)
        {
            var found = dependencies.TryGetValue(key, out var nodeList);

            if (!found)
            {
                nodeList = new List<Node<TItem>>();
                dependencies.Add(key, nodeList);
            }

            nodeList.Add(node);
        }

        public int Count => dependencies.Count;
    }
}
