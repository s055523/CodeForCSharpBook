using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UserModeConstructs
{
    class Program
    {
        public static volatile int number2;
        static void Main(string[] args)
        {
            CodeTimer.Initialize();

            CodeTimer.Time("NoLock", 100, Program.NoLock);
            CodeTimer.Time("Interlock", 100, Program.InterLock);
            CodeTimer.Time("SimpleSpinLock", 100, Program.SimpleSpinLock);
            CodeTimer.Time("MSSpinLock", 100, Program.MSSpinLock);
            Console.ReadKey();
        }

        public static void NoLock()
        {
            var number = 0;
            Parallel.For(0, 100000, (i) =>
            {
                number++;
            });           
        }
        public static void VolatileLock()
        {
            Parallel.For(0, 100000, (i) =>
            {
                number2++;
            });
            Console.WriteLine("Result: " + number2);
        }
        public static void InterLock()
        {
            var number = 0;
            Parallel.For(0, 100000, (i) =>
            {
                Interlocked.Add(ref number, 1);
            });
        }
        public static void SimpleSpinLock()
        {
            var ssl = new SimpleSpinLock();
            var number = 0;
            Parallel.For(0, 100000, (i) =>
            {
                ssl.Enter();
                number++;
                ssl.Exit();
            });
        }
        public static void MSSpinLock()
        {
            var ss2 = new SpinLock();
            var number = 0;
            Parallel.For(0, 100000, (i) =>
            {
                bool lockTaken = false;
                ss2.Enter(ref lockTaken);
                number++;
                ss2.Exit();
            });
        }

        public static int AtomicMultiply(ref int target, int multiplier)
        {
            int currentVal = target;   //将target的当前值保存到currentVal中
            int startVal, desiredVal;  //声明两个变量来记录操作开始前的值和期望的结果值

            do
            {
                startVal = currentVal; //将currentVal中的值保存到startVal中，此时记录的是target在操作开始前的最初值。
                desiredVal = currentVal * multiplier; //这是期望的结果

                //线程可能在这里被抢占,target的值可能被改变
                //如果target的值被改变了，那么target和startVal的值就不相等，所以就不应该用desiredVal替换target，因为结果也要跟着改变
                //如果target的值没有被改变，那么target和startVal的值就相等，使用desiredVal替换target。
                //不管替换或者不替换，CompareExchange的返回值始终是target的值，所以currentVal的值现在是target的最新值。

                //CompareExchange:将参数1和参数3的值比较，相等，则将第2个参数的值取代第一个，否则不操作。无论如何，都返回第一个参数之前的值
                currentVal = Interlocked.CompareExchange(ref target, desiredVal, startVal);
            }
            while (startVal != currentVal); //当target的起始值和最新值不相等的时候，说明target被修改了，所以继续下次判断，否则退出循环。
            return desiredVal;
        }
    }

    public class SimpleSpinLock
    {
        private int _flag;
        public void Enter()
        {
            //如果flag不等于1，则将它置为1并离开while循环（相当于进入临界区）
            //否则就一直在循环里面自旋，直到有一个线程把flag改成0为止
            while (Interlocked.Exchange(ref _flag, 1) != 0)
            {
                //性能优化代码，由于不停在自旋，所以可以适当减少这个线程的时间片？
            }
        }

        public void Exit()
        {
            //离开关键代码段，将flag置为0
            Thread.VolatileWrite(ref _flag, 0);
        }
    }
}
