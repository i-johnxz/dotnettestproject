using System;

namespace OpenClosedPrinciple
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }


    interface IAdapter
    {
        bool Request(string url);
    }

    class AjaxAdapter : IAdapter
    {
        public bool Request(string url)
        {
            throw new NotImplementedException();
        }
    }

    class NodeAdapter: IAdapter
    {
        public bool Request(string url)
        {
            throw new NotImplementedException();
        }
    }


    class HttpRequester
    {
        private readonly IAdapter _adapter;

        public HttpRequester(IAdapter adapter)
        {
            _adapter = adapter;
        }

        public bool Fetch(string url)
        {
            return _adapter.Request(url);
        }
    }
}
