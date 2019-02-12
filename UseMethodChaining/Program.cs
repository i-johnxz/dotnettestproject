using System;
using System.Collections.Generic;

namespace UseMethodChaining
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int>() {1, 2, 3, 4, 5}
                .FluentAdd(1)
                .FluentInsert(0, 0)
                .FluentRemoveAt(1)
                .FluentReverse()
                .FluentForEach(Console.WriteLine)
                .FluentClear();

            Console.WriteLine("Hello World!");
            Console.Read();
        }
    }
}
