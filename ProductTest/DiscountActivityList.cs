namespace ProductTest
{
    // 打折活动基类，支持多个商品同时结算
    class DiscountActivityList : BaseActivityList
    {
        private ActivityListProduct product = null;

        public DiscountActivityList(int discount, ActivityListProduct _product)
        {
            Discount = discount;
            product = _product;
        }
        // 折扣，比如 90 折 即为90
        public int Discount { get; set; }

        public override int GetPrice()
        {
            var productPrice = product.GetPrice();
            return productPrice * Discount / 100;
        }
    }
}