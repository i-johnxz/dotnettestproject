using System;
using System.Collections.Generic;
using System.Text;

namespace LiskovSubstitutionPrinciple
{
    class Square : ShapeBase
    {
        private double Length = 0;

        public double SetLength(double length)
        {
            return Length = length;
        }

        public override double GetArea()
        {
            return Math.Pow(Length, 2);
        }
    }
}
