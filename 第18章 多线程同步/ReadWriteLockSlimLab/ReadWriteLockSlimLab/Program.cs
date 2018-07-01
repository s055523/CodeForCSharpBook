using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReadWriteLockSlimLab
{
    class Program
    {
        static readonly ReaderWriterLockSlim rw = new ReaderWriterLockSlim();

        //一条争用的列表
        static List<string> list = new List<string>();
        static readonly Random r = new Random();

        static void Main(string[] args)
        {
            //开启三条读线程和一条写线程
            new Thread(Write).Start();
            new Thread(Read).Start();
            new Thread(Read).Start();
            new Thread(Read).Start();
        }

        static void Read()
        {
            while (true)
            {
                //先休息0.5秒
                Thread.Sleep(500);

                //如果在1秒之内无法获得读取的权利，就开始下个循环
                if (!rw.TryEnterReadLock(1000))
                {
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ": 由于被写线程占据，暂时无法读取列表。");
                    continue;
                }

                //获得了读取权，读取列表然后释放读取锁
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ": 现在列表成员：" + string.Join(",", list));
                rw.ExitReadLock();
            }
        }

        static void Write()
        {
            while (true)
            {
                //先休息1秒，此时读线程将可以读取列表
                Console.WriteLine("写线程在休息");
                Thread.Sleep(1000);

                //因为只有一条写线程所以肯定可以成功
                rw.EnterWriteLock();
                var next = r.NextDouble().ToString("N2");
                Console.WriteLine("写线程" + Thread.CurrentThread.ManagedThreadId + "正在写入" + next);
                list.Add(next);

                //占用写线程3秒，此时读线程将无法读取
                Thread.Sleep(3000);
                rw.ExitWriteLock();
            }
        }
    }

}
