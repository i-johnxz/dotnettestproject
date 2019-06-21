using System.Collections.Generic;

namespace ProductTest
{
    //商品列表的基类,用于活动结算使用
    class ActivityListProduct : List<BaseProduct>
    {
        //商品列表活动结算的方法，基类必须重写
        public virtual int GetPrice()
        {
            int ret = 0;
            base.ForEach(s => { ret += s.GetPrice(); });
            return ret;
        }
    }
}