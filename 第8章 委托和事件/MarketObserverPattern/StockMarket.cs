using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketObserverPattern
{
    class StockMarket : IObservable<int>
    {
        //观察者列表
        private List<IObserver<int>> people;

        public StockMarket()
        {
            people = new List<IObserver<int>>();
        }

        //实现IObservable<int>需要实现订阅方法
        public IDisposable Subscribe(IObserver<int> observer)
        {
            people.Add(observer);
            return null;
        }

        //退订
        public void UnSubscribe(IObserver<int> observer)
        {
            var person = (StockPeople) observer;
            if (people.Contains(observer))
            {
                Console.WriteLine(person.name + ":唉，干点别的吧，不玩股票了！");
                Console.WriteLine(person.name + "退出了股市");
                people.Remove(person);
            }
        }
        
        //生产N年的业绩数据
        public void PerformanceOverYears(int startyear, int n)
        {
            foreach (var i in Enumerable.Range(startyear, n))
            {
                var p = GeneratePerformance();
                Console.WriteLine(i + "年的业绩是:" + p);

                //激发事件，出现新的数据后，将它推送给所有观察者
                foreach(var person in people)
                {
                    person.OnNext(p);
                }

                //模拟减少一个观察者
                if (i == 2004)
                {
                    UnSubscribe(people[0]);
                }
            }
        }

        private int GeneratePerformance()
        {
            var r = new Random(DateTime.Now.Millisecond);
            return r.Next(80, 120);
        }
    }    
}
