using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreTPCTest
{
    public abstract class BillingDetail
    {
        public int Id { get; set; }

        public string Owner { get; set; }

        public string Number { get; set; }
    }
}
