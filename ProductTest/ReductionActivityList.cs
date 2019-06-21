using System.Collections.Generic;
using System.Linq;

namespace ProductTest
{
    // 满减的活动
    class ReductionActivityList : BaseActivityList
    {
        //满减的对应表
        private ActivityListProduct product = null;
        private Dictionary<int, int> reductMap = null;

        public ReductionActivityList(Dictionary<int, int> _reductMap, ActivityListProduct _product)
        {
            reductMap = _reductMap;
            product = _product;
        }

        //获取折扣之后的价格
        public override int GetPrice()
        {
            var productAmount = product.GetPrice();
            // 根据商品的总价获取到要减的价格
            var reductValue = reductMap.OrderByDescending(s => s.Key).FirstOrDefault(s => productAmount >= s.Key).Value;
            return productAmount - reductValue;
        }
    }
}