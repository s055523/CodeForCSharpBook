using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketObserverPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var a1 = new StockPeople("张三");
            var a2 = new StockPeople("李四");
            var a3 = new StockPeople("王五");

            var sm = new StockMarket();
            sm.Subscribe(a1);
            sm.Subscribe(a2);
            sm.Subscribe(a3);

            //开始产生数据
            sm.PerformanceOverYears(2002, 5);

            //减少一个观察者
            sm.UnSubscribe(a2);
            sm.PerformanceOverYears(2007, 1);

            Console.ReadKey();
        }
    }
}
