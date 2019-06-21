using System;
using System.Diagnostics;

namespace HashNode
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsistentHash h = new ConsistentHash(200, 5);
            h.AddNode(new Server() {IP = "192.168.1.1"});
            h.AddNode(new Server() {IP = "192.168.1.2"});
            h.AddNode(new Server() {IP = "192.168.1.3"});
            h.AddNode(new Server() {IP = "192.168.1.4"});
            h.AddNode(new Server() {IP = "192.168.1.5"});
            /*for (int i = 0; i < h.nodes.Length; i++)
            {
                if (h.nodes[i] != null)
                {
                    Console.WriteLine($"{i}===={h.nodes[i].VirtualNodeName}");
                }
            }*/
            Stopwatch w = new Stopwatch();
            w.Start();
            for (int i = 0; i < 100000; i++)
            {
                var aaa = h.GetNode("test1");
            }
            w.Stop();
            Console.WriteLine(w.ElapsedMilliseconds);

            Console.WriteLine("End");
            Console.Read();
        }
    }
}