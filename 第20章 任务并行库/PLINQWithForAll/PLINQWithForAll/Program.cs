using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLINQWithForAll
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = Enumerable.Range((int)Math.Pow(10, 8), 1000).Union(Enumerable.Range(2, 100));
            var sw = new Stopwatch();

            sw.Start();
            data.AsParallel()
                .Where(IsPrimeLow).ForAll(a => Console.Write(a + " "));
            sw.Stop();
            Console.WriteLine("默认情况：" + sw.ElapsedMilliseconds);

            sw.Restart();
            data.AsParallel().WithMergeOptions(ParallelMergeOptions.NotBuffered)
                .Where(IsPrimeLow).ForAll(a => Console.Write(a + " "));
            sw.Stop();
            Console.WriteLine("NotBuffered情况：" + sw.ElapsedMilliseconds);

            sw.Restart();
            data.AsParallel().WithMergeOptions(ParallelMergeOptions.FullyBuffered)
                .Where(IsPrimeLow).ForAll(a => Console.Write(a + " "));
            sw.Stop();
            Console.WriteLine("FullyBuffered情况：" + sw.ElapsedMilliseconds);

            Console.WriteLine("求解完毕");
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
