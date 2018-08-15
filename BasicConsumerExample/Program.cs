using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace BasicConsumerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var conf = new Dictionary<string, object>
            {
                {"group.id", "test-consumer-group"},
                {"bootstrap.servers", "10.100.7.46:9092"},
                {"auto.commit.interval.ms", 5000},
                {"auto.offset.reset", "earliest"}
            };

            using (var consumer = new Consumer<Null, string>(conf, null, new StringDeserializer(Encoding.UTF8)))
            {
                consumer.OnMessage += (sender, message) =>
                {
                    Console.WriteLine($"Read '{message.Value}' from: {message.TopicPartitionOffset}");
                };

                consumer.OnError += (sender, error) => { Console.WriteLine($"Error: {error}"); };

                consumer.OnConsumeError += (sender, message) =>
                {
                    Console.WriteLine($"Consume error ({message.TopicPartitionOffset}): {message.Error}");
                };

                consumer.Subscribe("my-topic");

                while (true)
                {
                    consumer.Poll(TimeSpan.FromMilliseconds(100));
                }

            }
        }
    }
}
