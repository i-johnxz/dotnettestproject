using System;
using System.Collections.Generic;
using System.Text;

namespace Borrowable.Models
{
    class Book : LibraryItem
    {

        private string _author;
        private string _title;


        public Book(string author, string title, int numCopies)
        {
            _author = author;
            _title = title;
            NumCopies = numCopies;
        }

        public override void Display()
        {
            Console.WriteLine("\n Book ------ ");
            Console.WriteLine(" Author: {0}", _author);
            Console.WriteLine(" Title: {0}", _title);
            Console.WriteLine(" # Copies:{0}", NumCopies);
        }
    }
}
