using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsCore.Models
{
    public class CreditCard : BillingDetail
    {
        public int CardType { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

    }
}
