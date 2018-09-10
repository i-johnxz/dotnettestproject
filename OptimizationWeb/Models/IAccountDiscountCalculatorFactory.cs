namespace OptimizationWeb.Models
{
    public interface IAccountDiscountCalculatorFactory
    {
        IAccountDiscountCalculator GetAccountDiscountCalculator(AccountStatus accountStatus);
    }
}