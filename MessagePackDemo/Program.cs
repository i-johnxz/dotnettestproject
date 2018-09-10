using System;
using MessagePack;

namespace MessagePackDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var mc = new MyClass
            {
                Age = 99,
                FirstName = "hoge",
                LastName = "huga"
            };

            var bytes = MessagePackSerializer.Serialize(mc);
            var mc2 = MessagePackSerializer.Deserialize<MyClass>(bytes);

            var json = MessagePackSerializer.ToJson(bytes);

            Console.WriteLine(json);

            Console.WriteLine("Hello World!");
        }
    }

    [MessagePackObject(keyAsPropertyName: true)]
    public class MyClass
    {

        public int Age { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [IgnoreMember]
        public string FullName => FirstName + LastName;
    }
}
