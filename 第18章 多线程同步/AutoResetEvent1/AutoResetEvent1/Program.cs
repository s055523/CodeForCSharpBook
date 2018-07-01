using System;
using System.Threading;

namespace AutoResetEvent1
{
    class Program
    {
        //如果参数为true，会抵消一次WaitOne，在本程序中就意味着子线程不会被阻塞
        static readonly EventWaitHandle _waitHandle = new AutoResetEvent(false);

        static void Main()
        {
            //开启一个子线程
            new Thread(Waiter).Start();

            //主线程休息1秒，在这期间，子线程会调用WaitOne等待其他线程给予信号
            Thread.Sleep(1000);

            //给予信号（基于同一个内核对象）
            Console.WriteLine("通知子线程继续运行");
            _waitHandle.Set();

            _waitHandle.Dispose();
            Console.ReadKey();
        }

        static void Waiter()
        {
            Console.WriteLine("子线程运行");
            _waitHandle.WaitOne();
            Console.WriteLine("子线程继续");
        }
    }
}
