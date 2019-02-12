using System;
using System.Text;

namespace RecursiveReplacementDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var key = "detail.main.name";

            var position = key.IndexOf('.');

            if (position >= 0 && position + 1 <= key.Length)
            {
                key = key.Substring(0, position) + '[' + key.Substring(position + 1) + "]";

                key = key.Replace(".", "][");
            }

            Console.WriteLine(key);
            Console.Read();
        }
    }
}
