namespace ProductTest
{
    class DiscountActivity : BaseActivity
    {
        private BaseProduct product = null;

        public DiscountActivity(int discount, BaseProduct _product)
        {
            Discount = discount;
            product = _product;
        }


        public int Discount { get; set; }

        public override int GetPrice()
        {
            return product.GetPrice() * Discount / 100;
        }
    }
}