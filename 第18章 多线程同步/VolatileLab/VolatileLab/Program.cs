using System.Threading;

namespace VolatileLab
{
    class Program
    {
        static void Main(string[] args)
        {
            bool complete = false;
            var t = new Thread(() =>
            {
                bool toggle = false;

                //读取了complete的值，此时为false，然后，就存入寄存器，导致看不到主线程对其的值的更改
                while (!complete)
                {
                    toggle = !toggle;

                    //强迫一次volatile读，这可以获得最新的值false
                    //Thread.MemoryBarrier();
                }
            });

            t.Start();

            //保证子线程进入while
            Thread.Sleep(1000);

            complete = true;
            //C#确保所有的写操作最终都会写入内存，所以这里可以不用加栅栏。
            //Thread.MemoryBarrier();

            //永远陷在while中，子线程永远不会结束
            t.Join();
        }

    }
}
