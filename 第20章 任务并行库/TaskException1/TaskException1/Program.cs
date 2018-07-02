using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskException1
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo1();
            Demo2();
            Console.ReadKey();
        }

        static void Demo2()
        {
            var task = Task.Run(() =>
            {
                throw new DivideByZeroException();
            });

            task.ContinueWith((t) =>
            {
                if (t.Status == TaskStatus.Faulted)
                {
                    foreach (var ex in t.Exception.InnerExceptions)
                    {
                        //可以看到异常信息了
                        Console.WriteLine(ex);
                    }
                }
            });
        }

        static void Demo1()
        {
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
                    Console.WriteLine(t.Status);
                });
            }
            catch (AggregateException ex)
            {
                //捕获不到
                Console.WriteLine("ex");
            }
        }
    }
}
