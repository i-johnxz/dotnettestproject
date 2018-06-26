namespace TestNRulesConsole.Model
{
    public class Order
    {
        public int Id { get; protected set; }

        public Customer Customer { get; protected set; }

        public int Quantity { get; protected set; }

        public double UnitPrice { get; protected set; }

        public double PercentDiscount { get; protected set; }

        public bool IsOpen { get; protected set; }

        public bool IsDiscounted => PercentDiscount > 0;

        public double Amount { get; protected set; }

        public Order(int id, Customer customer, int quantity, double unitPrice)
        {
            Id = id;
            Customer = customer;
            Quantity = quantity;
            UnitPrice = unitPrice;
            IsOpen = true;
        }

        public void ApplyDiscount(double percentDiscount)
        {
            PercentDiscount = percentDiscount;
        }
    }
}
