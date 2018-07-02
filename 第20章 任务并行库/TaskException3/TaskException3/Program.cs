using System;
using System.Threading.Tasks;

namespace TaskException3
{
    class Program
    {
        static void Main(string[] args)
        {
            //定义了一个带有两个分离子任务的父任务
            var task = Task.Run(() =>
            {
                var childTask1 = Task.Run(() =>
                {
                    try
                    {
                        Console.WriteLine("childTask1");
                        throw new DivideByZeroException();
                    }
                    catch (Exception e)
                    {
                        //就地处理异常
                        Console.WriteLine("子任务1的异常被就地处理:" + e.Message);
                    }
                });
                var childTask2 = Task.Run(() =>
                {
                    Console.WriteLine("childTask2");
                    throw new InvalidCastException();
                });
                //将子任务的异常上升到父任务
                childTask2.Wait();
            });

            //分离子任务无法这样做：什么都捕捉不到
            task.ContinueWith(t =>
            {
                //Faulted
                Console.WriteLine("父任务的状态：" + t.Status);

                //子任务的异常上升到父任务
                if (t.Status == TaskStatus.Faulted)
                {
                    foreach (var ex in t.Exception.Flatten().InnerExceptions)
                    {
                        Console.WriteLine("这里有一条来自子任务2的异常：" + ex);
                    }
                }
            });

            Console.WriteLine("主线程做其他事");
            Console.ReadKey();
        }
    }
}
