using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace UseSearchableNames
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person
            {
                Name = "John",
                Age = 42
            };

            var stream2 = new MemoryStream();
            var ser2 = new DataContractJsonSerializer(typeof(Person));
            ser2.WriteObject(stream2, person);

            stream2.Position = 0;
            var sr2 = new StreamReader(stream2);
            Console.WriteLine("JSON form of Data object:");
            Console.WriteLine(sr2.ReadToEnd());

            Console.Read();
        }
    }

    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }

    }
}
