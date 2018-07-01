using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadInterrupt
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new Thread(delegate ()
            {
                try
                {
                    //休息无穷长时间
                    Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadInterruptedException)
                {
                    Console.Write("被迫醒来!");
                }
                //子线程继续运行
                Console.WriteLine("继续运行");
            });

            //Unstarted
            Console.WriteLine(t.ThreadState);
            t.Start();

            Thread.Sleep(100);

            //子线程已经睡了，WaitSleepJoin
            Console.WriteLine(t.ThreadState);

            //强行唤醒线程，引发异常之后，子线程会继续运行下去
            t.Interrupt();

            Thread.Sleep(100);

            //运行完了，Stopped
            Console.WriteLine(t.ThreadState);
            Console.ReadKey();
        }

    }
}
