using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel2
{
    class Program
    {
        static void Main(string[] args)
        {
            var N = 1000001;
            var count1 = 0;
            var count2 = 0;

            //JIT
            IsPrime(2);

            var sw = new Stopwatch();
            sw.Start();
            for (int i = 2; i < N - 1; i++)
            {
                if (IsPrime(i)) count1++;
            }

            var time = sw.ElapsedMilliseconds;
            Console.WriteLine("不使用并行:" + time);
            Console.WriteLine("质数的个数:" + count1);

            sw.Restart();

            //上限是N-1，不包括N
            Parallel.For(2, N, i => {
                //使用线程安全的方式更新count2
                if (IsPrime(i))
                {
                    Interlocked.Add(ref count2, 1);
                }
            });

            time = sw.ElapsedMilliseconds;
            Console.WriteLine("使用并行:" + time);
            Console.WriteLine("质数的个数:" + count2);

            Parallel.ForEach(Partitioner.Create(2, 1000000),
                range =>
                {
                    Console.WriteLine("分段({0},{1} 循环开始时间：{2})", range.Item1, range.Item2, DateTime.Now.TimeOfDay);
                    for (int i = range.Item1; i < range.Item2; i++)
                    {
                        IsPrime(i);
                    }
                });


            Console.ReadKey();
        }

        /// <summary>
        /// 判定质数代码
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static bool IsPrime(int number)
        {
            if (number <= 0) throw new Exception("输入必须大于0");
            if (number == 1) throw new Exception("1不是质数也不是合数");

            //判断质数只需要检查到平方根
            int sqrt = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 2; i <= sqrt; i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

    }
}
