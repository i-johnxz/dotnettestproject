namespace OptimizationWeb.Models
{
    public interface IAccountDiscountCalculator
    {
        decimal ApplyDiscount(decimal price);
    }
}