using System;
using System.Collections.Generic;
using System.Text;

namespace TopologicalSorting
{
    public class ItemEqualityComparer : EqualityComparer<Item>
    {
        public override bool Equals(Item x, Item y)
        {
            return (x == null && y == null) || (x != null && y != null && x.Name == y.Name);
        }

        public override int GetHashCode(Item obj)
        {
            return obj == null ? 0 : obj.Name.GetHashCode();
        }
    }
}
