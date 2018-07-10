using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreTPCTest
{
    public class CreditCard : BillingDetail
    {
        public int CardType { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }
    }
}
