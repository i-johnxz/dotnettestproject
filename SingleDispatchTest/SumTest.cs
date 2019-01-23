using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SingleDispatchTest
{
    public class SumTest
    {
        [Fact]
        public void Test()
        {
            var list = new List<Person>()
            {
                new Person(){Age = 1},
                new Person(){Age = null},
                new Person(){Age = 3},
            };

            var result = list.Select(s => s.Age).Sum();

        }


       
    }

    public class Person
    {
        public int? Age { get; set; }
    }
}
