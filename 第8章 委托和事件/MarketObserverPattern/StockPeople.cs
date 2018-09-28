using System;

namespace MarketObserverPattern
{
    //实现IObserver<int> 
    public class StockPeople : IObserver<int>
    {
        //股民姓名
        public string name { get; set; }

        public StockPeople(string n)
        {
            name = n;
        }

        //当数据源数据用尽时执行，这里不需要什么处理
        public void OnCompleted()
        {           
        }

        //当数据源出现问题执行，这里不需要什么处理
        public void OnError(Exception error)
        {
        }

        //当数据源下一个数据可用时，调用该方法
        public void OnNext(int value)
        {
            Console.WriteLine(value > 100 ?
                                name + ":业绩不错啊，买买买！" :
                                name + ":今年业绩不行啊，不买了");
        }
    }
}
