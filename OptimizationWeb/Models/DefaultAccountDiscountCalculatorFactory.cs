using System;

namespace OptimizationWeb.Models
{
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
}