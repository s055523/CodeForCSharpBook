using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncExample2
{
    class Program
    {
        //调用方法
        public static void Main(string[] args)
        {
            Console.WriteLine("主线程编号: " + Thread.CurrentThread.ManagedThreadId);

            UseAsync(1190494759);

            Console.WriteLine("await返回之后，主线程马上向下运行");

            //这也是一种阻塞，所以可以看到其他函数的输出
            Console.ReadKey();
        }

        //调用异步操作的异步方法
        static async Task<bool> UseAsync(int number)
        {
            //虽然方法以async修饰，但在遇到await之前，还是同步运行的
            //此时仍然在主线程中
            Console.WriteLine("异步方法运行: " + Thread.CurrentThread.ManagedThreadId);

            try
            {
                var ret = await Task.Run(() => IsPrimeLowAsync(number));

                //由于结果无法立刻可用，方法会返回
                //后面的代码会等到结果可用时才会执行
                Console.WriteLine("异步方法await后面的代码运行，线程编号: " + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("使用async: " + (ret ? "是质数" : "是合数"));
                return ret;
            }
            //最常规的异常处理方式：异常将会从AggregateException中自动解包
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
                return false;
            }
        }

        //异步操作
        private static bool IsPrimeLowAsync(int number)
        {
            Console.WriteLine("异步操作线程编号: " + Thread.CurrentThread.ManagedThreadId);
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
