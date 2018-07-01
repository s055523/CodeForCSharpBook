using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DelegateAsync1
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo2();
        }

        static void Demo2()
        {
            var d = new IsPrimeSlowDelegate(IsPrimeSlow);

            //定义回调函数的委托
            var callback = new AsyncCallback(CallBack);

            //使用回调函数
            d.BeginInvoke(1190494759, callback, null);

            Console.WriteLine("主线程现在可以做其它事");
            Console.ReadKey();

        }

        static void Demo1()
        {
            var d = new IsPrimeSlowDelegate(IsPrimeSlow);

            //注意这里BeginInvoke的第二个参数为null - 没有使用回调函数
            var iar = d.BeginInvoke(1190494759, null, null);

            //获得结果，比起Threading方法简单了很多
            var ret = d.EndInvoke(iar);

            Console.WriteLine(ret);
            Console.ReadKey();
        }

        public delegate bool IsPrimeSlowDelegate(int number);
        private static bool IsPrimeSlow(int number)
        {
            if (number <= 0) throw new Exception("输入必须大于0");
            if (number == 1) throw new Exception("1不是质数也不是合数");

            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        //回调函数
        private static void CallBack(IAsyncResult iar)
        {
            Console.WriteLine("调用回调函数");

            //强行转换为AsyncResult类型
            var ar = (AsyncResult)iar;

            //AsyncDelegate的输入已经在主线程中定义好了，现在只需要调用EndInvoke，而调用EndInvoke需要对应类型的委托的一个实例，故新建一个
            var ba = (IsPrimeSlowDelegate)ar.AsyncDelegate;

            //获得结果
            Console.WriteLine(ba.EndInvoke(iar));
        }
    }
}
