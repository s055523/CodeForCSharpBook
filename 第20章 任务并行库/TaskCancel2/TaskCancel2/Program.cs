using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCancel2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("模拟取消任务：");

            //指派了一个取消令牌，使得主程序可以取消
            var cts = new CancellationTokenSource();
            var t = SimpleCancelDemo(cts);
            t.Start();

            //再创建一个任务等待SimpleCancelDemo结束
            Task.Run(() =>
            {
                try
                {
                    //若注解掉这句，则无法捕捉到异常
                    t.Wait();
                }
                catch (AggregateException ex)
                {
                    foreach (var exInnerException in ex.InnerExceptions)
                    {
                        Console.WriteLine("错误类型：" + exInnerException.GetType());
                        Console.WriteLine("错误信息：" + exInnerException.Message);
                    }
                }
            });

            //主线程运行到这里，停住，按下任意键之后取消任务
            var a = Console.ReadKey();
            cts.Cancel();

            Console.WriteLine("任务取消！");
            Console.ReadKey();
        }

        static Task SimpleCancelDemo(CancellationTokenSource cts)
        {
            var task = new Task(() =>
            {
                //未取消任务则无限循环下去
                while (true)
                {
                    Console.WriteLine("如果要取消任务，请按下任意键。。。");
                    Thread.Sleep(1000);
                    cts.Token.ThrowIfCancellationRequested();
                }
                //某些代码优化工具可能会警告：函数永远不返回
            });
            return task;
        }

    }
}
