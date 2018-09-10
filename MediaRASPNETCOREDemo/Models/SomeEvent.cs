using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace MediaRASPNETCOREDemo.Models
{
    public class SomeEvent : INotification
    {
        public string Message { get; set; }


        public SomeEvent(string message)
        {
            Message = message;
        }
    }
}
