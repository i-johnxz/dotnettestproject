using System;
using System.Threading;
using System.Threading.Tasks;

namespace DisruptorDemo
{
    class Program
    {
        private static readonly Random _random = new Random();
        private static readonly int _ringSize = 16;  // Must be multiple of 2


        static void Main(string[] args)
        {
            var disruptor = new Disruptor.Dsl.Disruptor<ValueEntry>(() => new ValueEntry(), _ringSize, TaskScheduler.Default);

            disruptor.HandleEventsWith(new ValueAdditionHandler(), new TestValueAdditionHandler());

            var ringBuffer = disruptor.Start();

            while (true)
            {
                long sequenceNo = ringBuffer.Next();

                ValueEntry entry = ringBuffer[sequenceNo];

                entry.Value = _random.Next();

                ringBuffer.Publish(sequenceNo);

                Console.WriteLine("Published entry {0}, value {1}", sequenceNo, entry.Value);

                Thread.Sleep(250);
            }
        }
    }
}
