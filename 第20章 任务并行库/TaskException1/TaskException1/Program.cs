using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskException1
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo1();
            Thread.Sleep(500);
            Demo2();
            Thread.Sleep(500);
            Demo3();

            Console.ReadKey();
        }

        static void Demo1()
        {
            //一个会抛出异常的任务
            var task = Task.Run(() =>
            {
                throw new DivideByZeroException();
            });

            try
            {
                //下面的代码被注解，导致异常不向上传播
                //task.Wait();

                //没有指定TaskContinuationOptions时，任务抛出异常也算完成，会继续运行下面的任务
                task.ContinueWith((t) =>
                {
                    //打印Faulted
                    Console.WriteLine("Demo1的任务状态：" + t.Status);
                });
            }
            catch (AggregateException ex)
            {
                //捕获不到
                Console.WriteLine("ex");
            }
        }

        static void Demo2()
        {
            //任务的异常处理方法一：在任务代码中捕捉异常
            var task = Task.Run(() =>
            {
                try
                {
                    throw new DivideByZeroException();
                }
                catch (Exception ex)
                {
                    //在任务内部处理异常
                    Console.WriteLine("Demo2的异常：" + ex);                    
                }
            });

            //没有指定TaskContinuationOptions时，任务抛出异常也算完成，会继续运行下面的任务
            task.ContinueWith((t) =>
            {
                //打印Faulted
                Console.WriteLine("Demo2的任务状态：" + t.Status);
            });
        }

        static void Demo3()
        {
            var task = Task.Run(() =>
            {
                throw new DivideByZeroException();
            });

            //任务的异常处理方法二：在任务的接续任务中根据前任务的状态捕捉异常
            task.ContinueWith((t) =>
            {
                if (t.Status == TaskStatus.Faulted)
                {
                    foreach (var ex in t.Exception.InnerExceptions)
                    {
                        //可以看到异常信息了
                        Console.WriteLine("Demo3的异常：" + ex);
                    }
                }
            });
        }
    }
}
