using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCancel3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("模拟执行任务，取消任务或出现异常：");
            CancelDemo();
            Console.ReadKey();
        }

        static void CancelDemo()
        {
            var cts = new CancellationTokenSource();

            //模拟一个随机数字
            //大于67则成功，小于34则取消，其余情况抛出异常
            var r = new Random();
            var a = r.Next(100);

            Console.WriteLine("线程ID：" + Thread.CurrentThread.ManagedThreadId + ", 随机数字" + a);

            var task = Task.Factory.StartNew(() =>
            {
                //模拟任务的初始化并保证创建任务的线程运行完了
                Thread.Sleep(1000);

                //如果随机数字小于34则取消
                cts.Token.ThrowIfCancellationRequested();

                while (true)
                {
                    //模拟长任务
                    Thread.Sleep(3000);

                    //成功了
                    if (a > 67)
                    {
                        return a;
                    }
                    //抛出异常
                    if (a > 33)
                    {
                        throw new Exception("你运气太差。");
                    }
                }
            }, cts.Token);

            //回调
            SuccessCallBack(task);
            FailCallBack(task);
            ExceptionCallBack(task);

            //通过CancellationToken取消
            if (a <= 33)
            {
                cts.Cancel();
            }
        }

        static void SuccessCallBack<T>(Task<T> t)
        {
            t.ContinueWith(t1 =>
            {
                Console.WriteLine("任务成功了");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        static void FailCallBack<T>(Task<T> t)
        {
            t.ContinueWith(t1 =>
            {
                Console.WriteLine("任务取消了");
            }, TaskContinuationOptions.OnlyOnCanceled);
        }

        static void ExceptionCallBack<T>(Task<T> t)
        {
            t.ContinueWith(t1 =>
            {
                Console.WriteLine("任务出现异常");
                foreach (var ex in t.Exception.Flatten().InnerExceptions)
                {
                    //捕捉到异常并输出异常内容
                    Console.WriteLine(ex);
                }

            }, TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}
