using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinqToRxLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataFlow = Observable.Range(2, 99);

            var numbers = dataFlow.Where(d => IsPrime(d))
                .Subscribe(x => Console.Write("{0} ", x));

            Console.ReadKey();
        }

        public static bool IsPrime(int number)
        {
            for(int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }



    //处理器对int序列比较感兴趣
    class Processor2 : IObserver<int>
    {
        public void OnCompleted()
        {
            Console.WriteLine("数据已经处理完毕");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("数据不符合格式");
        }

        public void OnNext(int value)
        {
            Console.WriteLine("当前最新数据是：" + value);
        }
    }

    class DataCenter : IObservable<int>
    {
        private List<IObserver<int>> processors;

        public DataCenter()
        {
            processors = new List<IObserver<int>>();
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            processors.Add(observer);
            return null;
        }

        public void ProcessNextData()
        {
            var data = new Random().Next(2, int.MaxValue - 1);
            foreach(var proc in processors)
            {
                proc.OnNext(data);
            }

        }
    }
}
