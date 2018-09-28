using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLab
{
    class StockEvent
    {
        public delegate void SeePerformanceEventHandler(int p);
        public static event SeePerformanceEventHandler SeePerformance;

        static void Main(string[] args)
        {
            var a1 = new StockPeople("张三");
            var a2 = new StockPeople("李四");
            var a3 = new StockPeople("王五");

            //var handler = new SeePerformanceEventHandler(a1.SeePerformance);
            //handler += a2.SeePerformance;

            SeePerformance += a1.SeePerformance;
            SeePerformance += a2.SeePerformance;
            SeePerformance += a3.SeePerformance;

            foreach (var i in Enumerable.Range(2002, 10))
            {
                var p = GeneratePerformance();
                Console.WriteLine(i + "年的业绩是:" + p);

                //激发事件
                SeePerformance(p);
            }

            Console.ReadKey();
        }

        public static int GeneratePerformance()
        {
            var r = new Random(DateTime.Now.Millisecond);
            return r.Next(80, 120);
        }
    }

    class StockPeople
    {
        //股民姓名
        public string name { get; set; }

        public StockPeople(string n)
        {
            name = n;
        }

        //股民的响应
        public void SeePerformance(int performance)
        {
            Console.WriteLine(performance > 100 ?
                                name + ":业绩不错啊，买买买！" :
                                name + ":今年业绩不行啊，不买了");
        }
    }
}
