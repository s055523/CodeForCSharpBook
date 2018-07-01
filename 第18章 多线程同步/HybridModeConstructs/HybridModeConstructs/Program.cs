using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HybridModeConstructs
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeTimer.Time("SimpleHybridLock", 10, SimpleHybridLock);
            CodeTimer.Time("SimpleMonitor", 10, SimpleMonitor);
            CodeTimer.Time("MSMonitor", 10, MSMonitor);
            Console.ReadKey();
        }

        public static void SimpleHybridLock()
        {
            var l = new SimpleHybridLock();
            var number = 0;
            Parallel.For(0, 100000, (i) =>
            {
                l.Enter();
                number++;
                l.Exit();
            });
            Console.WriteLine(number);
        }

        public static void SimpleMonitor()
        {
            var l = new SimpleMonitor();
            var number = 0;
            Parallel.For(0, 100000, (i) =>
            {
                l.Enter();
                number++;
                l.Exit();
            });
            Console.WriteLine(number);
        }

        public static void MSMonitor()
        {
            int number = 0;
            object o = new object();
            Parallel.For(0, 100000, (i) =>
            {
                //Monitor.Enter(o);
                lock (o)
                {
                    number++;
                }
                //Monitor.Exit(o);
            });
        }

    }


    public class SimpleHybridLock : IDisposable
    {
        private int waiters = 0;

        //和内核锁不同，这里要使用false，因为Enter方法采用原子操作判断，第一个线程会直接获得锁离开，不会调用WaitOne
        private readonly Lazy<AutoResetEvent> are = new Lazy<AutoResetEvent>(() => new AutoResetEvent(false));

        public void Enter()
        {
            //没有争用时，将int变量增加1，从而获得锁
            //当大部分时候都没有争用时，不需要呼叫WaitOne，这是一个内核模式构造的方法，它会影响性能
            //相比之下，用户模式的原子操作速度快得多，这里吸收了用户模式的优点
            if (Interlocked.Increment(ref waiters) == 1) return;

            //发生争用时，阻塞当前线程，直到收到通知
            //阻塞而不是自旋，这里吸收了内核模式的优点
            are.Value.WaitOne();
        }

        public void Exit()
        {
            //理由同上，这里就不重复了
            if (Interlocked.Decrement(ref waiters) == 0) return;
            are.Value.Set();
        }

        public void Dispose()
        {
            are.Value.Dispose();
        }
    }

    public class SimpleMonitor : IDisposable
    {
        private int waiters = 0;
        private readonly Lazy<AutoResetEvent> are = new Lazy<AutoResetEvent>(() => new AutoResetEvent(false));

        //谁拥有锁？
        private int currentThreadId;

        //拥有多少次？
        private int count;

        //自行指定的一个自旋时间
        private int spinningTime = 4000;

        public void Enter()
        {
            //如果调用线程已经拥有锁则递增递归次数，该线程无条件获得锁
            if (currentThreadId == Thread.CurrentThread.ManagedThreadId)
            {
                count++;
                return;
            }

            //性能优化
            var spinwait = new SpinWait();
            for (int spinCount = 0; spinCount < spinningTime; spinCount++)
            {
                //Interlocked.CompareExchange方法有三个参数，它比较第一个和第三个参数的值
                //如果它们相等，将第一个参数的值替换为第二个参数的值
                //并且返回第一个参数的原始值
                //所以下面的代码的意思是：如果waiters的初始值等于0，则将waiters替换成1
                //如果if成立，意味着waiters是从0变为1的
                //也就意味着当前没有人持有锁
                if (Interlocked.CompareExchange(ref waiters, 1, 0) == 0)
                {
                    currentThreadId = Thread.CurrentThread.ManagedThreadId;
                    count = 1;
                    return;
                }
                //自旋一段极短的时间
                spinwait.SpinOnce();
            }

            //自旋时间结束之后仍然没有获得锁，意味着假设“锁只会被线程持有很短的时间”失败
            //此时只能转为内核模式，不能再自旋下去了，阻塞！
            if (Interlocked.Increment(ref waiters) > 1)
                are.Value.WaitOne();

            //线程被解除阻塞，现在它拥有锁！
            currentThreadId = Thread.CurrentThread.ManagedThreadId;
            count = 1;
        }

        public void Exit()
        {
            //调用的线程不拥有锁，表明存在一个bug
            if (Thread.CurrentThread.ManagedThreadId != currentThreadId)
                throw new SynchronizationLockException("调用的线程必须拥有锁");

            //减少递归锁的递归次数，如果这个线程多次拥有锁，则返回
            if (--count > 0) return;

            //重置拥有锁的线程id，现在没有线程拥有锁
            currentThreadId = 0;

            //同上，这里就不重复了
            if (Interlocked.Decrement(ref waiters) == 0) return;
            are.Value.Set();
        }

        public void Dispose()
        {
            are.Value.Dispose();
        }
    }
}
