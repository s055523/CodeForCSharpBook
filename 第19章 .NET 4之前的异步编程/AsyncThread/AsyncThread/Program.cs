using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncThread
{
    class Program
    {
        public static string result;

        static void Main(string[] args)
        {
            Demo4();
        }

        /// <summary>
        /// 最基本的情况
        /// </summary>
        static void Demo1()
        {
            var thread = new Thread(() => IsPrimeSlow(1190494759));
            thread.Start();

            //没有结果提示
            Console.ReadKey();
        }

        static void Demo2()
        {
            var thread = new Thread(() => IsPrimeSlow2(1190494759));
            thread.Start();

            Console.ReadKey();

            //得凭感觉，只有一次机会！
            Console.WriteLine(result);
            Console.ReadKey();
        }

        static void Demo3()
        {
            var thread = new Thread(() => IsPrimeSlow2(1190494759));
            thread.Start();

            while (thread.ThreadState != ThreadState.Stopped)
            {
                //这个等待方法比较愚蠢
                Thread.Sleep(100);
            }
            Console.WriteLine(result);
            Console.ReadKey();
        }

        static void Demo4()
        {
            var thread = new Thread(() => IsPrimeSlow3(1190494759));
            thread.Start();
            Console.ReadKey();
        }

        private static void IsPrimeSlow3(int number)
        {
            if (number <= 0) throw new Exception("输入必须大于0");
            if (number == 1) throw new Exception("1不是质数也不是合数");

            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    PrimeCallback("false");
                    return;
                }
            }
            PrimeCallback("true");
        }

        private static void PrimeCallback(string result)
        {
            Console.WriteLine(result);
        }


        /// <summary>
        /// 效率极低的判定质数代码
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static bool IsPrimeSlow(int number)
        {
            if (number <= 0) throw new Exception("输入必须大于0");
            if (number == 1) throw new Exception("1不是质数也不是合数");

            //故意注解掉这行使得效率大大下降 
            //int sqrt = (int) Math.Floor(Math.Sqrt(number));

            for (int i = 2; i < number; i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        private static void IsPrimeSlow2(int number)
        {
            if (number <= 0) throw new Exception("输入必须大于0");
            if (number == 1) throw new Exception("1不是质数也不是合数");

            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    result = "false";
                    return;
                }
            }
            result = "true";
        }
    }
}
