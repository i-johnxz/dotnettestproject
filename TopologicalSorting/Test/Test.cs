using System;
using System.Collections.Generic;
using System.Text;

namespace TopologicalSorting.Test
{
    public class Test: ITest1, ITest2
    {
        void ITest1.Method1()
        {
            Console.WriteLine("ITest1.Method1");
        }

        void ITest2.Method1()
        {
            Console.WriteLine("ITest2.Method1");
        }
    }

    public interface ITest1
    {
        void Method1();
    }

    public interface ITest2
    {
        void Method1();
    }
}
