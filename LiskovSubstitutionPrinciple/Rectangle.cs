using System;
using System.Collections.Generic;
using System.Text;

namespace LiskovSubstitutionPrinciple
{
    class Rectangle : ShapeBase
    {

        public void SetWidth(double width)
        {
            Width = width;
        }

        public void SetHeight(double height)
        {
            Height = height;
        }

        public override double GetArea()
        {
            return Width * Height;
        }
    }
}
