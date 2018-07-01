using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PLINQWithCancel
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var data = Enumerable.Range(2, 999998).ToArray();

            Task.Run(() =>
            {
                Thread.Sleep(1000);
                cts.Cancel();
            });

            try
            {
                var allPrimeNumbers = data.AsParallel().WithCancellation(cts.Token).Where(IsPrimeLow);

                //强制PLINQ执行（不过，会被另一个线程取消）
                Console.WriteLine(allPrimeNumbers.Count());
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine("已经被取消");
            }

            Console.ReadKey();
        }
        private static bool IsPrimeLow(int number)
        {
            if (number <= 0) throw new Exception("输入必须大于0");
            if (number == 1) return false;

            //判断质数只需要检查到平方根
            //int sqrt = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 2; i < number; i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

    }
}
