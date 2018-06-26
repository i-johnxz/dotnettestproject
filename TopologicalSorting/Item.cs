using System;
using System.Collections.Generic;
using System.Text;

namespace TopologicalSorting
{
    public class Item
    {
        public string Name { get; protected set; }

        public Item[] Dependencies { get; protected set; }

        public Item(string name, params Item[] dependencies)
        {
            Name = name;
            Dependencies = dependencies;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
