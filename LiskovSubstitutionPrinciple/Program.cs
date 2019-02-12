using System;

namespace LiskovSubstitutionPrinciple
{
    class Program
    {
        static void Main(string[] args)
        {
            ShapeBase[] shapes = {new Rectangle(), new Rectangle(), new Square()};

            RenderLargeRectangles(shapes);

            
            Console.WriteLine("Hello World!");
        }

        static Drawable RenderLargeRectangles(ShapeBase[] shapeBases)
        {
            foreach (var shapeBase in shapeBases)
            {
                if (shapeBase is Square square)
                {
                    square.SetLength(5);
                }
                else if (shapeBase is Rectangle rectangle)
                {
                    rectangle.SetWidth(4);
                    rectangle.SetHeight(5);
                }

                var area = shapeBase.GetArea();
                shapeBase.Render(area);
            }

            return new Drawable();
        }
    }
}
