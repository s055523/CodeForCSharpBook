using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLINQPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            //JIT
            IsPrime(2);
            var data = Enumerable.Range(2, 499998).ToArray();

            var sw = new Stopwatch();
            sw.Start();

            //强制使用块分区 - 使用较差的算法，使得不同元素耗时差距增大
            var allPrimeNumbers = Partitioner.Create(data, true).AsParallel().Where(IsPrimeLow);
            Console.WriteLine("质数的个数（块分区）：" + allPrimeNumbers.Count());
            Console.WriteLine(sw.ElapsedMilliseconds);

            sw.Restart();

            //allPrimeNumbers是数组所以默认使用范围分区
            allPrimeNumbers = data.AsParallel().Where(IsPrimeLow);
            Console.WriteLine("质数的个数（范围分区）：" + allPrimeNumbers.Count());
            Console.WriteLine(sw.ElapsedMilliseconds);

            sw.Restart();

            //强制使用块分区 - 使用较好的算法
            allPrimeNumbers = Partitioner.Create(data, true).AsParallel().Where(IsPrime);
            Console.WriteLine("质数的个数（块分区）：" + allPrimeNumbers.Count());
            Console.WriteLine(sw.ElapsedMilliseconds);

            sw.Restart();

            //allPrimeNumbers是数组所以默认使用范围分区
            allPrimeNumbers = data.AsParallel().Where(IsPrime);
            Console.WriteLine("质数的个数（范围分区）：" + allPrimeNumbers.Count());
            Console.WriteLine(sw.ElapsedMilliseconds);

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

        private static bool IsPrime(int number)
        {
            if (number <= 0) throw new Exception("输入必须大于0");
            if (number == 1) return false;

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
