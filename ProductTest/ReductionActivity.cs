using System.Collections.Generic;
using System.Linq;

namespace ProductTest
{
    class ReductionActivity : BaseActivity
    {
        private BaseProduct product = null;
        
        // 满减的对应表
        private Dictionary<int, int> reductMap = null;


        public ReductionActivity(Dictionary<int, int> _redutMap, BaseProduct _product)
        {
            reductMap = _redutMap;
            product = _product;
        }
        
        //获取折扣之后的价格
        public override int GetPrice()
        {
            var productAmount = product.GetPrice();
            //根据商品的总价获取到要减的价格
            var reductValue = reductMap.OrderByDescending(s => s.Key).FirstOrDefault(s => productAmount >= s.Key).Value;
            return productAmount - reductValue;
        }
    }
}