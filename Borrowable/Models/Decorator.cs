using System;
using System.Collections.Generic;
using System.Text;

namespace Borrowable.Models
{
    abstract class Decorator : LibraryItem
    {

        protected LibraryItem LibraryItem;

        protected Decorator(LibraryItem libraryItem)
        {
            LibraryItem = libraryItem;
        }

        public override void Display()
        {
            LibraryItem.Display();
        }
    }
}
