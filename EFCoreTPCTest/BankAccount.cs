using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreTPCTest
{
    public class BankAccount : BillingDetail
    {
            
        public string BankName { get; set; }

        public string Swift { get; set; }
    }
}
