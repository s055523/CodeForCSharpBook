using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KernelModeConstructs
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeTimer.Time("AutoResetEventLock", 10, AutoResetEventLock);
            CodeTimer.Time("SemaphoreLock", 10, SemaphoreLock);
            CodeTimer.Time("SemaphoreSlim", 10, SemaphoreSlimExample);
            CodeTimer.Time("MutexLock", 10, MutexLock);
            Console.ReadKey();
        }

        public static void AutoResetEventLock()
        {
            var l = new AutoResetEventLock();
            var number = 0;
            Parallel.For(0, 100000, (i) =>
            {
                l.Enter();
                number++;
                l.Exit();
            });
            Console.WriteLine(number);
        }
        public static void SemaphoreLock()
        {
            var l = new SemaphoreLock();
            var number = 0;
            Parallel.For(0, 100000, (i) =>
            {
                l.Enter();
                number++;
                l.Exit();
            });
            Console.WriteLine(number);
        }
        public static void SemaphoreSlimExample()
        {
            var l = new SemaphoreSlim(1);
            var number = 0;
            Parallel.For(0, 100000, (i) =>
            {
                l.Wait();
                number++;
                l.Release();
            });
            Console.WriteLine(number);
        }
        public static void MutexLock()
        {
            int number = 0;
            var m = new Mutex();
            Parallel.For(0, 100000, (i) =>
            {
                m.WaitOne();
                number++;
                m.ReleaseMutex();
            });
            Console.WriteLine(number);
        }
    }

    public class AutoResetEventLock : IDisposable
    {
        private readonly AutoResetEvent _are = new AutoResetEvent(true);

        public void Enter()
        {
            //AutoResetEvent参数为true，使第一个到来的线程无条件通过WaitOne
            //然后，就自动呼叫Reset，第二个到来的线程将会阻塞
            _are.WaitOne();
        }
        public void Exit()
        {
            _are.Set();
        }
        public void Dispose()
        {
            _are.Dispose();
        }
    }

    public class SemaphoreLock : IDisposable
    {
        //当前值1，最大值1，和其他内核模式实现锁的原理一样，一开始必须允许一条线程可以无条件获得锁
        //如果设置当前值为0，则程序永远不会结束
        private readonly Semaphore s = new Semaphore(1, 1);

        public void Enter()
        {
            //消耗一个信号量
            s.WaitOne();
        }
        public void Exit()
        {
            //增加一个信号量
            s.Release(1);
        }
        public void Dispose()
        {
            s.Dispose();
        }
    }
}
