using System;
using System.Collections.Generic;
using System.Text;

namespace TopologicalSorting
{
    public class Item2
    {
        public string Name { get; protected set; }

        public string[] Dependencies { get; protected set; }

        public Item2(string name,params string[] dependencies)
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
