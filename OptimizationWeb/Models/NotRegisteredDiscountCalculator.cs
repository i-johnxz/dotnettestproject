namespace OptimizationWeb.Models
{
    public class NotRegisteredDiscountCalculator : IAccountDiscountCalculator
    {
        public decimal ApplyDiscount(decimal price)
        {
            return price;
        }
    }
}