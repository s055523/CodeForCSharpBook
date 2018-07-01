using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TLS1
{
    public class Test
    {
        [ThreadStatic]
        public static int a;  //TLS变量 

        [ThreadStatic]
        public static string b;  //TLS变量
    }

    class Program
    {
        static void Main()
        {
            Test.a = 1000;
            Test.b = "hello";
            Console.WriteLine("子线程未运行时，主线程：" + Test.a);
            Console.WriteLine("子线程未运行时，主线程：" + Test.b);

            var worker = new Thread(Go);
            worker.Start();
            worker.Join();

            //主线程的a和b不会改变，因为它们是TLS变量
            Console.WriteLine("子线程运行完毕之后主线程：" + Test.a);
            Console.WriteLine("子线程运行完毕之后主线程：" + Test.b);
            Console.ReadKey();
        }

        static void Go()
        {
            Test.a = 999;
            Test.b = "world";
            Console.WriteLine("子线程中修改了：" + Test.a);
            Console.WriteLine("子线程中修改了：" + Test.b);
        }
    }

}
