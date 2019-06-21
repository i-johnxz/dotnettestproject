namespace ProductTest
{
    class VirtualProduct: BaseProduct
    {
        public override int GetPrice()
        {
            return this.Price;
        }
    }
}