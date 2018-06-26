using System;
using System.Linq;
using TopologicalSorting.Test;

namespace TopologicalSorting
{
    class Program
    {
        static void Main(string[] args)
        {
            //var a = new Item("A");
            //var c = new Item("C");
            //var f = new Item("F");
            //var h = new Item("H");
            //var d = new Item("D", new Item("A"));
            //var g = new Item("G", new Item("F"), new Item("H"));
            //var e = new Item("E", new Item("D", new Item("A")), new Item("G", new Item("F"), new Item("H")));
            //var b = new Item("B", new Item("C"), new Item("E", new Item("D", new Item("A")), new Item("G", new Item("F"), new Item("H"))));

            //var unsorted = new[] {a, b, c, d, e, f, g, h};

            //var sorted = TopologicalSort.Sort(unsorted, x => x.Dependencies, x => x.Name);
            //Console.WriteLine(string.Join(",", sorted));
            
            //var a = new Item2("A");
            //var b = new Item2("B", "C", "E");
            //var c = new Item2("C");
            //var d = new Item2("D", "A");
            //var e = new Item2("E", "D", "G");
            //var f = new Item2("F");
            //var g = new Item2("G", "F", "H");
            //var h = new Item2("H");

            //var unsorted = new[] {a, b, c, d, e, f, g, h};

            //var sorted = TopologicalSort.Sort(unsorted, x => x.Dependencies, x => x.Name);
            //Console.WriteLine(string.Join(",", sorted));

            //var a = new Item("A");
            //var c = new Item("C");
            //var f = new Item("F");
            //var h = new Item("H");
            //var d = new Item("D", a);
            //var g = new Item("G", f, h);
            //var e = new Item("E", d, g);
            //var b = new Item("B", c, e);

            //var unsorted = new[] {a, b, c, d, e, f, g, h};

            //var sorted = TopologicalSort.Group(unsorted, x => x.Dependencies);

            //Console.WriteLine(string.Join(",", sorted));

            var a = new TestItem("A");
            var b = new TestItem("B", "C", "E");
            var c = new TestItem("C");
            var d = new TestItem("D", "A");
            var e = new TestItem("E", "D", "G");
            var f = new TestItem("F");
            var g = new TestItem("G", "F", "H");
            var h = new TestItem("H");

            var unsorted = new[] {a, b, c, d, e, f, g, h};
            var expected = new[] {a, c, d, f, h, g, e, b};

            var actual = unsorted.TopoSort(x => x.Name, x => x.Dependencies).ToArray();


            Console.Read();
        }
        
    }

    public class TestItem
    {
        public string Name { get; protected set; }

        public string[] Dependencies { get; protected set; }

        public TestItem(string name,params string[] dependencies)
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
