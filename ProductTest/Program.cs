using System;
using System.Collections.Generic;

namespace ProductTest
{
    class Program
    {
        static void Main(string[] args)
        {

            /*
            VirtualProduct p = new VirtualProduct() {Price = 1000};
            // 打折活动
            DiscountActivity da = new DiscountActivity(90, p);

            var retPrice = da.GetPrice();
            Console.WriteLine($"打折后的价格{retPrice}");
            // 还能叠加参加满减活动
            Dictionary<int, int> m = new Dictionary<int, int> {{200, 5}, {300, 10}, {500, 20}, {1000, 50}};

            ReductionActivity ra = new ReductionActivity(m, da);
            retPrice = ra.GetPrice();
            Console.WriteLine($"打折满减后的价格{retPrice}");
            
            ReductionActivity ra2 = new ReductionActivity(m, ra);
            retPrice = ra2.GetPrice();
            Console.WriteLine($"再打折后的价格{retPrice}");
*/

            VirtualProduct p = new VirtualProduct() {Price = 1000};
            VirtualProduct p2 = new VirtualProduct() {Price = 1000};
            ActivityListProduct lst = new ActivityListProduct();
            lst.Add(p);
            lst.Add(p2);
            
            DiscountActivityList dalist = new DiscountActivityList(80, lst);
            Console.WriteLine($"打折后的价格{dalist.GetPrice()}");
            DiscountActivityList dalist2 = new DiscountActivityList(90, dalist);
            Console.WriteLine($"打折后的价格{dalist2.GetPrice()}");
            
            DiscountActivityList dalist3 = new DiscountActivityList(90, dalist2);
            Console.WriteLine($"打折后的价格{dalist3.GetPrice()}");
            
            // 还能叠加参加满减活动
            Dictionary<int, int> m = new Dictionary<int, int>();
            m.Add(200, 5);
            m.Add(300, 10);
            m.Add(500, 20);
            m.Add(1000, 50);
            
            ReductionActivityList ral = new ReductionActivityList(m, dalist3);
            Console.WriteLine($"再满减打折后的价格{ral.GetPrice()}");

            Console.WriteLine("End");
        }
    }
}