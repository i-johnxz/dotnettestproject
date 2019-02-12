using System;
using System.Collections.Generic;
using System.Text;

namespace LiskovSubstitutionPrinciple
{
    abstract class ShapeBase
    {
        protected double Width = 0;
        protected double Height = 0;

        public abstract double GetArea();

        public Drawable Render(double area)
        {
            return new Drawable();
        }
    }



}
