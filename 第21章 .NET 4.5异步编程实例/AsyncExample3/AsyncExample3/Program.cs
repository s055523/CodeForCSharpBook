using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncExample3
{
    class Program
    {
        //调用方法
        public static void Main(string[] args)
        {
            Console.WriteLine("主线程编号: " + Thread.CurrentThread.ManagedThreadId);
            var cts = new CancellationTokenSource();
            UseAsync(1190494759, cts);

            Console.WriteLine("按任意键取消");
            Console.ReadKey();
            cts.Cancel();

            Console.ReadKey();
        }

        //调用异步操作的异步方法
        static async Task<bool> UseAsync(int number, CancellationTokenSource cts)
        {
            //虽然方法以async修饰，但在遇到await之前，还是同步运行的
            //此时仍然在主线程中
            Console.WriteLine("异步方法运行: " + Thread.CurrentThread.ManagedThreadId);

            try
            {
                var ret = await Task.Run(() => IsPrimeLowAsync(number, cts.Token));

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
            catch (OperationCanceledException)
            {
                Console.WriteLine("任务取消！");
                return false;
            }
        }

        //异步操作
        private static bool IsPrimeLowAsync(int number, CancellationToken ct)
        {
            Console.WriteLine("异步操作线程编号: " + Thread.CurrentThread.ManagedThreadId);
            if (number <= 0) throw new ArgumentException("输入必须大于0");
            if (number == 1) return false;

            for (int i = 2; i < number; i++)
            {
                ct.ThrowIfCancellationRequested();
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}
