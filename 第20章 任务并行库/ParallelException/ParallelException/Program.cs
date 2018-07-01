using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelException
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Parallel.Invoke(Run1, Run2);
            }
            //可以收集到两条异常
            catch (AggregateException ex)
            {
                foreach (var s in ex.InnerExceptions)
                {
                    Console.WriteLine(s);
                }
            }
            //catch (Exception ex)
            //{
            //    //只显示“我是任务2抛出的异常”
            //    Console.WriteLine(ex.InnerException); 
            //}
            Console.Read();
        }

        static void Run1()
        {
            Thread.Sleep(300);
            throw new Exception("我是任务1抛出的异常");
        }

        static void Run2()
        {
            Thread.Sleep(500);
            throw new Exception("我是任务2抛出的异常");
        }

    }
}
