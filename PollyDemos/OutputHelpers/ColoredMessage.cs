using System;
using System.Collections.Generic;
using System.Text;

namespace PollyDemos.OutputHelpers
{
    public class ColoredMessage
    {
        public Color Color { get; }

        public String Message { get; }


        public ColoredMessage(string message, Color color)
        {
            Color = color;
            Message = message;
        }

        public ColoredMessage(string message) : this(message, Color.Default)
        {

        }
    }
}
