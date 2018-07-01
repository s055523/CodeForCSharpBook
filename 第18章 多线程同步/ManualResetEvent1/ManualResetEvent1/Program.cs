using System;
using System.Threading;

namespace ManualResetEvent1
{
    class Program
    {
        //一开始门是关着的
        static readonly ManualResetEvent mre = new ManualResetEvent(false);

        static void Main()
        {
            var t1 = new Thread(Method);
            var t2 = new Thread(Method);
            var t3 = new Thread(Method);

            t1.Start();
            t2.Start();
            t3.Start();

            Console.WriteLine("Ready?");
            Thread.Sleep(1000);            

            Console.WriteLine("Go!");
            mre.Set();

            Console.ReadKey();
            mre.Dispose();
        }

        static void Method()
        {
            //进入等待队列，开门之前不会向下运行
            mre.WaitOne();
            Console.WriteLine("子线程运行");
        }
    }
}
