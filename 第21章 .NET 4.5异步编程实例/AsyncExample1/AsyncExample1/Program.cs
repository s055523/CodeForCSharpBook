using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncExample1
{
    class Program
    {
        static void Main(string[] args)
        {
            //无论是使用任务还是async/await，主线程都不会被阻塞
            UseTask(1190494759);
            Console.WriteLine("向后运行");
            UseAsync(1190494759);
            Console.WriteLine("向后运行2");

            Console.ReadKey();
        }

        static bool UseTask(int number)
        {
            var ret = false;

            //立刻开始一个异步任务
            Task.Run(() => IsPrimeLow(number)).ContinueWith(t =>
            {
                //获得结果
                Console.WriteLine("使用任务: " + (t.Result ? "是质数" : "是合数"));
                ret = t.Result;
            });
            return ret;
        }

        static async Task<bool> UseAsync(int number)
        {
            //立刻开始一个异步任务
            //使用await等待它的结果
            //await可以看成是ContinueWith的语法糖
            //因此，可以把这句话看成ContinueWith之后，将结果赋值给ret
            var ret = await Task.Run(() => IsPrimeLow(number));
            Console.WriteLine("使用async: " + (ret ? "是质数" : "是合数"));
            return ret;
        }

        private static bool IsPrimeLow(int number)
        {
            if (number <= 0) throw new ArgumentException("输入必须大于0");
            if (number == 1) return false;

            for (int i = 2; i < number; i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}
