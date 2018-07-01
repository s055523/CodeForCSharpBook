using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskException2
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = ChildExceptionDemo();
            t.ContinueWith(task =>
            {
                //附加子任务的异常默认向上传播
                if (t.Status == TaskStatus.Faulted)
                {
                    foreach (var ex in t.Exception.Flatten().InnerExceptions)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            });

            t.Start();
            Console.ReadKey();
        }

        static Task ChildExceptionDemo()
        {
            //定义了一个带有两个附加子任务的父任务
            var task = new Task(() =>
            {
                var childTask1 = new Task(() =>
                {
                    throw new DivideByZeroException();
                });
                var childTask2 = new Task(() =>
                {
                    throw new InvalidCastException();
                });
                childTask1.Start();
                childTask2.Start();
            });
            return task;
        }

    }
}
