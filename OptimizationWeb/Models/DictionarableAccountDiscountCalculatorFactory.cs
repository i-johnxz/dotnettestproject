using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizationWeb.Models
{
    public class DictionarableAccountDiscountCalculatorFactory : IAccountDiscountCalculatorFactory
    {
        private readonly Dictionary<AccountStatus, Lazy<IAccountDiscountCalculator>> _discountCalculators;

        public DictionarableAccountDiscountCalculatorFactory(Dictionary<AccountStatus, Lazy<IAccountDiscountCalculator>> discountCalculators)
        {
            _discountCalculators = discountCalculators;
        }


        public IAccountDiscountCalculator GetAccountDiscountCalculator(AccountStatus accountStatus)
        {
            if (!_discountCalculators.TryGetValue(accountStatus, out var calculator))
            {
                throw new NotImplementedException("There is not implementation of IAccountDiscountCalculatoryFactory interface for given Account status");
            }

            return calculator;
        }
    }
}
