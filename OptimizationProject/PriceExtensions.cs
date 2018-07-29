using System;
using System.Collections.Generic;
using System.Text;

namespace OptimizationProject
{
    public static class PriceExtensions
    {
        public static decimal ApplyDiscountForAccountStatus(this decimal price, decimal discountSize)
        {
            return price - (discountSize * price);
        }

        public static decimal ApplyDiscountForTimeOfHavingAccount(this decimal price, int timeOfHavingAccountInYears)
        {

            decimal discountForLoyaltyInPercentage =
                (timeOfHavingAccountInYears > Constants.MAXIMUM_DISCOUNT_FOR_LOYALTY)
                    ? (decimal) Constants.MAXIMUM_DISCOUNT_FOR_LOYALTY / 100
                    : (decimal) timeOfHavingAccountInYears / 100;

            return price - (discountForLoyaltyInPercentage * price);
        }
    }
}
