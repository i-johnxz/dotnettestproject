using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace BasicProduerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Dictionary<string, object>
            {
                {"bootstrap.servers", "10.100.7.46:9092"}
            };

            using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
            {
                

                while (true)
                {
                    Console.WriteLine("请输入字符串:");
                    var text = Console.ReadLine();
                    var dr = producer.ProduceAsync("my-topic", null, text).Result;
                    Console.WriteLine($"Delivered '{dr.Value}' to : {dr.TopicPartitionOffset}");
                }
            }
        }
    }
}
