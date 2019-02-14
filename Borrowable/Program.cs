using System;
using Borrowable.Models;

namespace Borrowable
{
    class Program
    {
        static void Main(string[] args)
        {
            Book book = new Book("Morley", "Inside ASP.NET", 10);
            book.Display();

            Video video = new Video("Spielberg", "Jaws", 23, 92);
            video.Display();

            Console.WriteLine("\n Making video borrowable:");

            Models.Borrowable borrowvideo = new Models.Borrowable(video);
            borrowvideo.BorrowItem("Customer #1");
            borrowvideo.BorrowItem("Customer #2");

            borrowvideo.Display();

            Console.Read();
        }
    }
}
