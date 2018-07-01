using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLINQ1
{
    class Program
    {
        static void Main(string[] args)
        {
            //使用AsParallel实现PLINQ查询
            var allPrimeNumbers = Enumerable.Range(2, 999998).AsParallel().Where(IsPrime);

            Console.WriteLine("质数的个数：" + allPrimeNumbers.Count());
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
