using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadAsyncCancellation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("模拟取消任务：");

            //取消对象
            var ch = new CancelHelper();

            //将取消对象传入任务
            new Thread(() =>
            {
                try
                {
                    Task(ch);
                }
                catch (OperationCanceledException ex)
                {
                    //捕捉到异常
                    Console.WriteLine("任务取消！");
                }
            }).Start();

            //按下任意键之后取消任务
            Console.ReadKey();
            ch.Cancel();
            
            Console.ReadKey();
        }

        static void Task(CancelHelper ch)
        {
            while (true)
            {
                ch.ThrowIfCancellationRequested();
                Console.WriteLine("如果要取消任务，请按下任意键。。。");
                Thread.Sleep(1000);
            }
        }
    }
}
