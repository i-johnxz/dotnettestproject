namespace ProductTest
{
    abstract class BaseProduct
    {
        // 商品价格，单位：分
        public int Price { get; set; }

        // 获取商品价格抽象方法
        public abstract int GetPrice();
    }
}