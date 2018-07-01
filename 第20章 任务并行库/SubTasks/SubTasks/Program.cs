using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SubTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo2();
        }

        static void Demo1()
        {
            var parentTask = new Task<int>(() =>
            {
                //主任务创建了一个子任务
                new Task(() =>
                {
                    Thread.Sleep(3000);
                    Console.WriteLine("子任务结束");
                }).Start();
                return 1;
            });

            //主任务的接续
            parentTask.ContinueWith(t =>
            {
                Console.WriteLine("主任务的接续");
            });

            parentTask.Start();
            Console.ReadKey();
        }

        static void Demo2()
        {
            var parentTask = new Task<int>(() =>
            {
                //主任务创建了一个附加子任务
                new Task(() =>
                {
                    Thread.Sleep(3000);
                    Console.WriteLine("子任务结束");
                }, TaskCreationOptions.AttachedToParent).Start();
                return 1;
            });

            //由于主任务存在附加子任务，主任务的接续必须等到所有附加子任务完成之后
            //才会运行
            parentTask.ContinueWith(t =>
            {
                Console.WriteLine("主任务的接续");
            });

            parentTask.Start();
            Console.ReadKey();
        }

    }
}
