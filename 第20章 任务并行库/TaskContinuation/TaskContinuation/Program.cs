using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskContinuation
{
    class Program
    {
        static void Main(string[] args)
        {
            //Demo1();
            //Demo2();
            var task = Task.Factory.StartNew(() =>
            {
                var a = new Random().Next(0, 100);
                Thread.Sleep(1000);
                if (a > 50)
                {
                    //模拟任务失败
                    throw new Exception();
                }
                return a;
            });
            SuccessCallBack(task);
            FailCallBack(task);

            Console.ReadKey();

        }

        static void Demo1()
        {
            var task = Task.Run(() =>
            {
                int a = 1;
                Thread.Sleep(3000);
                return a + 10;
            }).ContinueWith(t =>
            {
                Console.WriteLine("任务的结果是" + t.Result);
            });

            //主线程继续作其他事情
            Console.ReadKey();
        }

        static void Demo2()
        {
            var task = Task.Run(() =>
            {
                int a = 1;
                Thread.Sleep(3000);
                return a + 10;
            });
            CallBack(task);

            Console.ReadKey();
        }

        static void CallBack<T>(Task<T> t)
        {
            //使用任务工厂定义一个接续任务（只能使用任务工厂）
            var f = new TaskFactory();
            var task2 = f.ContinueWhenAll(new[] { t }, task =>
            {
                Console.WriteLine("任务的结果是" + task[0].Result);
            });
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
                Console.WriteLine("任务失败了");
            }, TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}
