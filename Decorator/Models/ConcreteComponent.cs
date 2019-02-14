using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator.Models
{
    class ConcreteComponent: Component
    {
        public override void Operation()
        {
            Console.WriteLine("ConcreteComponent.Operation()");
        }
    }
}
