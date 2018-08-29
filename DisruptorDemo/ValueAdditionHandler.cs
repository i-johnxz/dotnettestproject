using System;
using System.Collections.Generic;
using System.Text;
using Disruptor;
using Disruptor.Dsl;

namespace DisruptorDemo
{
    public class ValueAdditionHandler : IEventHandler<ValueEntry>
    {
        public void OnEvent(ValueEntry data, long sequence, bool endOfBatch)
        {
            Console.WriteLine("Event handled: Value = {0}, processed event {1}, endOfBatch event {2}", data.Value,
                sequence, endOfBatch);
        }
    }

    public class TestValueAdditionHandler : IEventHandler<ValueEntry>
    {
        public void OnEvent(ValueEntry data, long sequence, bool endOfBatch)
        {
            Console.WriteLine("TestValueAdditionHandler Event handled: Value = {0}, processed event {1}, endOfBatch event {2}", data.Value,
                sequence, endOfBatch);
        }
    }
}
