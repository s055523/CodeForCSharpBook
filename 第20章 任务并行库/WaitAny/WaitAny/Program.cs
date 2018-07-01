using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WaitAny
{
    class Program
    {
        static void Main(string[] args)
        {
            var taskList = new List<Task>
            {
                Task.Run(() =>
                {
                    int a = 1;
                    Thread.Sleep(3000);
                    return a + 10;
                }),
                Task.Run(() =>
                {
                    int a = 1;
                    Thread.Sleep(1000);
                    return a + 10;
                })
            };

            //等待任意一个任务完成
            Task.WaitAny(taskList.ToArray());

            Console.WriteLine("至少一个任务运行完成了");
            Console.ReadKey();
        }
    }
}
