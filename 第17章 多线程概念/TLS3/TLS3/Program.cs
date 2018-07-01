using System;
using System.Threading;

namespace TLS3
{
    public class Test
    {
        public int a;
        public string b;
    }

    class Program
    {
        static void Main()
        {
            //创建ThreadLocal<Test>对象并设置默认值
            //每个线程都将拥有一个Test对象t的独立副本
            //对它的访问也会自动使用TLS
            var t = new ThreadLocal<Test>(() => new Test
            {
                a = 1000,
                b = "hello"
            });

            Console.WriteLine("子线程未运行时，主线程：" + t.Value.a);
            Console.WriteLine("子线程未运行时，主线程：" + t.Value.b);

            var worker = new Thread(() => Go(t));
            worker.Start();
            worker.Join();

            //仍然是原来的值
            Console.WriteLine("子线程运行完毕之后主线程：" + t.Value.a);
            Console.WriteLine("子线程运行完毕之后主线程：" + t.Value.b);
            Console.ReadKey();
        }

        static void Go(ThreadLocal<Test> t)
        {
            //子线程会修改它自己TLS上t的值
            t.Value.a = 999;
            t.Value.b = "world";

            Console.WriteLine("子线程中修改了：" + t.Value.a);
            Console.WriteLine("子线程中修改了：" + t.Value.b);
        }
    }
}
