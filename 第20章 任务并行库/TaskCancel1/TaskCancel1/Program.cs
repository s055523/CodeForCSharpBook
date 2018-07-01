using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCancel1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("模拟取消任务：");

            //指派了一个取消令牌，使得主程序可以取消
            var cts = new CancellationTokenSource();
            SimpleCancelDemo(cts);

            //按下任意键之后取消任务
            var a = Console.ReadKey();
            cts.Cancel();

            Console.WriteLine("任务取消！");
            Console.ReadKey();
        }

        static void SimpleCancelDemo(CancellationTokenSource cts)
        {
            var task = Task.Factory.StartNew(() =>
            {
                //未取消任务则无限循环下去
                while (!cts.IsCancellationRequested)
                {
                    Console.WriteLine("如果要取消任务，请按下任意键。。。");
                    Thread.Sleep(1000);
                    
                    //在这里选择是否抛出异常
                    cts.Token.ThrowIfCancellationRequested();
                }
            }, cts.Token).ContinueWith(t =>
            {
                //打印RanToCompletion
                Console.WriteLine("取消之后任务的状态：" + t.Status);
            });
        }

    }
}
