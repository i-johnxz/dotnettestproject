using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RecursiveReplacementDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var key = "detail.main.name";

            var position = key.IndexOf('.');

            var newKey = string.Empty;

            if (position >= 0 && position + 1 <= key.Length)
            {
                newKey = key + ']';
                var regex = new Regex(Regex.Escape("."));

                var newText = regex.Replace(newKey, "[", 1);
                newKey = regex.Replace(newText, "][");
            }


            

            Console.WriteLine(newKey);
            Console.Read();
        }
        
        
    }
}
