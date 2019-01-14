using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TagHelperDemo.Models
{
    public class Person
    {
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        public string LastName { get; set; }

        public int Age { get; set; }
        
        [DisplayName("Email address")]
        public string EmailAddress { get; set; }
    }
}
