using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator.Models
{
    abstract class Decorator: Component
    {
        protected Component Component;

        public void SetComponent(Component component)
        {
            this.Component = component;
        }

        public override void Operation()
        {
            Component?.Operation();
        }
    }
}
