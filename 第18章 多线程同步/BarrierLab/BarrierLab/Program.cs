using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BarrierLab
{
    class Program
    {
        //一个拥有3个参与者的屏障
        //必须被调用SignalAndWait三次，屏障才能破除
        //Meeting是屏障的接续动作
        static readonly Barrier b = new Barrier(3, b => Meeting());

        static void Main()
        {
            Console.WriteLine("人到齐了才能开会");

            new Thread(Arrival1).Start();
            new Thread(Arrival2).Start();
            new Thread(Arrival3).Start();

            Console.ReadKey();
            b.Dispose();
        }

        static void Arrival1()
        {
            Thread.Sleep(1000);
            Console.WriteLine("张三到了");
            b.SignalAndWait();
        }
        static void Arrival2()
        {
            Thread.Sleep(2000);
            Console.WriteLine("李四到了");
            b.SignalAndWait();
        }
        static void Arrival3()
        {
            Thread.Sleep(3000);
            Console.WriteLine("领导到了");
            b.SignalAndWait();
        }
        static void Meeting()
        {
            Console.WriteLine("现在可以开会了");
        }
    }
}
