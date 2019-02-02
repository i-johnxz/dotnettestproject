using System;
using Ceras;

namespace CerasConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            // 1.) Simple usage
            // aka. "I'm here for the cool features! I want to optimize for max-performance later"
            var person = new Person { Name = "riki", Health = 100 };

            var serializer = new CerasSerializer();
            

            var data = serializer.Serialize(person);
            data.VisualizePrint("Simple Person");

            var clone1 = serializer.Deserialize<Person>(data);
            Console.WriteLine($"Clone: Name={clone1.Name}, Health={clone1.Health}");
            Console.Read();
        }
    }



}
