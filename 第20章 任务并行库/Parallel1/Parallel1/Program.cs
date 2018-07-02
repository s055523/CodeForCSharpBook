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

            //使用并行
            Parallel.Invoke(Task1, Task2, Task3);
            var time = sw.ElapsedMilliseconds;
            Console.WriteLine("使用并行:" + time);

            sw.Restart();

            //串行
            Task1();
            Task2();
            Task3();
            time = sw.ElapsedMilliseconds;
            Console.WriteLine("不使用并行:" + time);

            Console.ReadKey();
        }

        static void Task1()
        {
            Thread.Sleep(1000);
        }
        static void Task2()
        {
            Thread.Sleep(800);
        }
        static void Task3()
        {
            Thread.Sleep(1200);
        }

    }
}
