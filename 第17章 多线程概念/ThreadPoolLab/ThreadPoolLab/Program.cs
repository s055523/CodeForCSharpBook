using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolLab
{
    class Program
    {
        static void Main()
        {
            var count = 0;
            var count2 = 0;
            //将线程池最大的线程数量设置为10（普通线程和IO线程）
            ThreadPool.SetMaxThreads(10, 10);

            ThreadPool.GetMaxThreads(out count, out count2);
            Console.WriteLine("最大可能线程数量: " + count);
            Console.WriteLine("最大可能IO线程数量: " + count);

            for (int i = 0; i < 15; i++)
            {
                ThreadPool.QueueUserWorkItem(Go, i);
            }
            Console.ReadKey();
        }

        static void Go(object data)
        {
            Console.WriteLine("任务" + data + "开始执行");
            Thread.Sleep(20000);
            Console.WriteLine("任务" + data + "执行完了");
        }

    }
}
