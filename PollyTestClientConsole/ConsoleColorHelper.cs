using System;
using System.Collections.Generic;
using System.Text;
using PollyDemos.OutputHelpers;

namespace PollyTestClientConsole
{
    public static class ConsoleColorHelper
    {
        public static ConsoleColor ToConsoleColor(this Color color)
        {
            switch (color)
            {
                case Color.White:
                case Color.Default:
                    return ConsoleColor.White;
                case Color.Green:
                    return ConsoleColor.Magenta;
                case Color.Magenta:
                    return ConsoleColor.Red;
                case Color.Yellow:
                    return ConsoleColor.Yellow;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
