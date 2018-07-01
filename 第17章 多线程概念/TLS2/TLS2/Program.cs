using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TLS2
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
            var t = new Test
            {
                a = 1000,
                b = "hello"
            };

            //将a放入槽中，它的值1000将不会被其他线程影响
            Thread.SetData(Thread.GetNamedDataSlot("slot1"), t.a);

            Console.WriteLine("子线程未运行时，主线程：" + t.a);
            Console.WriteLine("子线程未运行时，主线程：" + t.b);

            var worker = new Thread(() => Go(t));
            worker.Start();
            worker.Join();

            //把数据拿出来
            var originalData = Thread.GetData(Thread.GetNamedDataSlot("slot1"));

            //仍然是1000
            Console.WriteLine("子线程运行完毕之后主线程：" + (int)originalData);

            //t.b会被子线程影响
            Console.WriteLine("子线程运行完毕之后主线程：" + t.b);
            Console.ReadKey();
        }

        static void Go(Test t)
        {
            //子线程无法访问其他线程的数据槽，因此，这句代码会出错，因为子线程的slot1没有数据
            //var a = (int) Thread.GetData(Thread.GetNamedDataSlot("slot1"));
            t.a = 999;
            t.b = "world";

            Console.WriteLine("子线程中修改了：" + t.a);
            Console.WriteLine("子线程中修改了：" + t.b);
        }
    }
}
