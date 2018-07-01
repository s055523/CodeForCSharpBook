using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel1
{
    class Program
    {
        static void Main(string[] args)
        {
            //JIT 
            Task1();
            Task2();
            Task3();

            var sw = new Stopwatch();
            sw.Start();

            //使用TPL
            Parallel.Invoke(Task1, Task2, Task3);
            var time = sw.ElapsedMilliseconds;
            Console.WriteLine("使用TPL:" + time);

            sw.Restart();

            //串行
            Task1();
            Task2();
            Task3();
            time = sw.ElapsedMilliseconds;
            Console.WriteLine("不使用TPL:" + time);

            Console.ReadKey();
        }

        static void Task1()
        {
            Thread.Sleep(10);
        }
        static void Task2()
        {
            Thread.Sleep(8);
        }
        static void Task3()
        {
            Thread.Sleep(12);
        }

    }
}
