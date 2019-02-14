using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator.Models
{
    class ConcreteDecoratorA : Decorator
    {
        public override void Operation()
        {
            base.Operation();
            Console.WriteLine("ConcreteDecoratorA.Operation()");
        }
    }
}
