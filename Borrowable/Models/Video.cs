﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Borrowable.Models
{
    class Video : LibraryItem
    {

        private string _director;
        private string _title;
        private int _playTime;

        public Video(string director, 
                     string title, 
                     int numCopies, 
                     int playTime)
        {
            _director = director;
            _title = title;
            NumCopies = numCopies;
            _playTime = playTime;
        }

        public override void Display()
        {
            Console.WriteLine("\n Video ----- ");
            Console.WriteLine(" Director: {0}", _director);
            Console.WriteLine(" Title: {0}", _title);
            Console.WriteLine(" # Copies: {0}", NumCopies);
            Console.WriteLine(" Playtime: {0}\n ", _playTime);
        }
    }
}
