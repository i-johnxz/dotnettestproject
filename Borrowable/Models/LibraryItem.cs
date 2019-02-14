using System;
using System.Collections.Generic;
using System.Text;

namespace Borrowable.Models
{
    abstract class LibraryItem
    {
        public int NumCopies { get; set; }

        public abstract void Display();
    }
}
