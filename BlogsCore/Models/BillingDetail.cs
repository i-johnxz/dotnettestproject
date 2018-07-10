using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsCore.Models
{
    public abstract class BillingDetail
    {
        public int Id { get; set; }

        public string Owner { get; set; }

        public string Number { get; set; }
    }
}
