using System;

namespace HttpClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = new Uri("http://roxa.teamcore.cn/");

            var baseUrl = new Uri(url, "/uploads/1.png");


            Console.WriteLine(baseUrl.ToString());
            Console.Read();
        }
    }
}
