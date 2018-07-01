using System;
using System.Threading;

namespace AutoResetEvent2
{
    class Program
    {
        static readonly AutoResetEvent main = new AutoResetEvent(false);
        static readonly AutoResetEvent subThread = new AutoResetEvent(false);

        //把message设计为全局的，令子线程有办法访问到
        static string _message;

        static void Main()
        {
            //创建一个新的后台线程
            _message = "task 1";

            //启动后台线程
            Console.WriteLine("主线程：启动后台线程");
            new Thread(Work).Start();

            Thread.Sleep(1000);

            subThread.Set();

            //等待后台线程的通知
            main.WaitOne();

            //下一个任务
            _message = "task 2";
            subThread.Set();
            main.WaitOne();

            //令后台线程终止            
            Console.WriteLine("令后台线程终止");
            _message = null;
            subThread.Set();

            Console.ReadKey();
            main.Dispose();
            subThread.Dispose();
        }

        static void Work()
        {
            while (true)
            {
                Console.WriteLine("后台线程待命");
                subThread.WaitOne();

                if (_message == null) return;
                Console.WriteLine("后台线程正在工作");

                //任务
                Thread.Sleep(1000);

                Console.WriteLine("后台线程工作完了，任务结果：" + _message);
                main.Set();
            }
        }
    }
}
