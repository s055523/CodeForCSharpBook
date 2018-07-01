using System;
using System.Threading;

namespace ThreadLab
{
    class Program
    {
        static void Main()
        {
            Thread.CurrentThread.Name = "主线程";
            var worker = new Thread(Go) { Name = "工作线程" };
            worker.Start();
            Go();

            Console.ReadKey();
        }

        static void Go()
        {
            Console.WriteLine("我是" + Thread.CurrentThread.Name);
        }
    }

}
