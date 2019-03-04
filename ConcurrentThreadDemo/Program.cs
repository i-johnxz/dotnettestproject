using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConcurrentThreadDemo
{
    class Program
    {
        static Task Main(string[] args)
        {
            var urls = new[] {
                "https://github.com/naudio/NAudio",
                "https://twitter.com/mark_heath",
                "https://github.com/markheath/azure-functions-links",
                "https://pluralsight.com/authors/mark-heath",
                "https://github.com/markheath/advent-of-code-js",
                "http://stackoverflow.com/users/7532/mark-heath",
                "https://mvp.microsoft.com/en-us/mvp/Mark%20%20Heath-5002551",
                "https://github.com/markheath/func-todo-backend",
                "https://github.com/markheath/typescript-tetris",
            };
            var client = new HttpClient();
            foreach (var url in urls)
            {
                var html = await client.GetStringAsync(url);
                Console.WriteLine($"retrieved {html.Length} characters from {url}");
            }
        }
    }
}
