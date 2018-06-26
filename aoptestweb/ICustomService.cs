using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aoptestweb.Attributes;

namespace aoptestweb
{
    public interface ICustomService
    {
        [CustomInterceptor]
        void Call();
    }

    public class CustomService : ICustomService
    {
        public void Call()
        {
            Console.WriteLine("service calling...");
        }
    }
}
