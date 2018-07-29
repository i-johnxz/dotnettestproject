using System;
using System.Collections.Generic;
using System.Text;

namespace OptimizationProject
{
    public class DiscountManager
    {
        //public decimal ApplyDiscount(decimal price, int accountStatus, int timeOfHavingAccountInYears)
        //{
        //    decimal priceAfterDiscount = 0;
        //    decimal discountForLoyaltyInPercentage = (timeOfHavingAccountInYears > 5)
        //        ? (decimal) 5 / 100
        //        : (decimal) timeOfHavingAccountInYears / 100;
        //    if (accountStatus == 1)
        //    {
        //        priceAfterDiscount = price;
        //    }
        //    else if (accountStatus == 2)
        //    {
        //        priceAfterDiscount = (price - (0.1m * price)) -
        //                             (discountForLoyaltyInPercentage * (price - (0.1m * price)));
        //    }
        //    else if (accountStatus == 3)
        //    {
        //        priceAfterDiscount = (0.7m * price) - (discountForLoyaltyInPercentage * (0.7m * price));
        //    }
        //    else if (accountStatus == 4)
        //    {
        //        priceAfterDiscount = (price - (0.5m * price)) -
        //                             (discountForLoyaltyInPercentage * (price - (0.5m * price)));
        //    }

        //    return priceAfterDiscount;
        //}

        //public decimal ApplyDiscount(decimal price, AccountStatus accountStatus, int timeOfHavingAccountInYears)
        //{
        //    decimal priceAfterDiscount = 0;


        //    switch (accountStatus)
        //    {
        //        case AccountStatus.NotRegistered:
        //            priceAfterDiscount = price;
        //            break;
        //        case AccountStatus.SimpleCustomer:
        //            priceAfterDiscount = price.ApplyDiscountForAccountStatus(Constants.DISCOUNT_FOR_SIMPLE_CUSTOMERS);

        //            break;
        //        case AccountStatus.ValuableCustomer:
        //            priceAfterDiscount = price.ApplyDiscountForAccountStatus(Constants.DISCOUNT_FOR_VALUABLE_CUSTOMERS);

        //            break;
        //        case AccountStatus.MostValuableCustomer:
        //            priceAfterDiscount =
        //                price.ApplyDiscountForAccountStatus(Constants.DISCOUNT_FOR_MOST_VALUABLE_CUSTOMERS);

        //            break;
        //        default:
        //            throw new NotImplementedException();
        //    }

        //    priceAfterDiscount = priceAfterDiscount.ApplyDiscountForTimeOfHavingAccount(timeOfHavingAccountInYears);
        //    return priceAfterDiscount;
        //}


        private readonly IAccountDiscountCalculatorFactory _factory;
        private readonly ILoyaltyDiscountCalculator _loyaltyDiscountCalculator;

        public DiscountManager(IAccountDiscountCalculatorFactory factory, ILoyaltyDiscountCalculator loyaltyDiscountCalculator)
        {
            _factory = factory;
            _loyaltyDiscountCalculator = loyaltyDiscountCalculator;
        }

        public decimal ApplyDiscount(decimal price, AccountStatus accountStatus, int timeOfHavingAccountInYears)
        {
            decimal priceAfterDiscount = 0;
            priceAfterDiscount = _factory.GetAccountDiscountCalculator(accountStatus).ApplyDiscount(price);
            priceAfterDiscount = _loyaltyDiscountCalculator.ApplyDiscount(priceAfterDiscount, timeOfHavingAccountInYears);
            return priceAfterDiscount;
        }
    }


    public interface ILoyaltyDiscountCalculator
    {
        decimal ApplyDiscount(decimal price, int timeOfHavingAccountInYears);
    }

    public class DefaultLoyaltyDiscountCalculator : ILoyaltyDiscountCalculator
    {
        public decimal ApplyDiscount(decimal price, int timeOfHavingAccountInYears)
        {
            decimal discountForLoyaltyInPercentage =
                (timeOfHavingAccountInYears > Constants.MAXIMUM_DISCOUNT_FOR_LOYALTY)
                    ? (decimal) Constants.MAXIMUM_DISCOUNT_FOR_LOYALTY / 100
                    : (decimal) timeOfHavingAccountInYears / 100;
            return price - (discountForLoyaltyInPercentage * price);
        }
    }


    public interface IAccountDiscountCalculatorFactory
    {
        IAccountDiscountCalculator GetAccountDiscountCalculator(AccountStatus accountStatus);
    }

    public class DefaultAccountDiscountCalculatorFactory : IAccountDiscountCalculatorFactory
    {
        public IAccountDiscountCalculator GetAccountDiscountCalculator(AccountStatus accountStatus)
        {
            IAccountDiscountCalculator calculator;
            switch (accountStatus)
            {
                case AccountStatus.NotRegistered:
                    calculator = new NotRegisteredDiscountCalculator();
                    break;
                case AccountStatus.SimpleCustomer:
                    calculator = new SimpleCustomerDiscountCalculator();
                    break;
                case AccountStatus.ValuableCustomer:
                    calculator = new ValuableCustomerDiscountCalculator();
                    break;
                case AccountStatus.MostValuableCustomer:
                    calculator = new MostValuableCustomerDiscountCalculator();
                    break;
                default:
                    throw new NotImplementedException();
            }

            return calculator;
        }
    }

    public interface IAccountDiscountCalculator
    {
        decimal ApplyDiscount(decimal price);
    }

    public class NotRegisteredDiscountCalculator : IAccountDiscountCalculator
    {
        public decimal ApplyDiscount(decimal price)
        {
            return price;
        }
    }

    public class SimpleCustomerDiscountCalculator : IAccountDiscountCalculator
    {
        public decimal ApplyDiscount(decimal price)
        {
            return price - (Constants.DISCOUNT_FOR_SIMPLE_CUSTOMERS * price);
        }
    }

    public class ValuableCustomerDiscountCalculator : IAccountDiscountCalculator
    {
        public decimal ApplyDiscount(decimal price)
        {
            return price - (Constants.DISCOUNT_FOR_VALUABLE_CUSTOMERS * price);
        }
    }

    public class MostValuableCustomerDiscountCalculator: IAccountDiscountCalculator
    {
        public decimal ApplyDiscount(decimal price)
        {
            return price - (Constants.DISCOUNT_FOR_MOST_VALUABLE_CUSTOMERS * price);
        }
    }
}

