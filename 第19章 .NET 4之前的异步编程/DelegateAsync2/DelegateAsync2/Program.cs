using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;

namespace DelegateAsync2
{
    class Program
    {
        static void Main(string[] args)
        {
            var d = new IsPrimeSlowDelegate(IsPrimeSlow);

            //定义回调函数的委托
            var callback = new AsyncCallback(CallBack);

            //取消令牌
            var cts = new CancellationTokenSource();

            //使用回调函数和取消令牌
            d.BeginInvoke(1, cts.Token, callback, null);

            Console.WriteLine("如果要取消任务，请按下任意键。。。");

            //按下任意键之后取消任务
            Console.ReadKey();
            cts.Cancel();

            Console.ReadKey();
        }

        //加入取消令牌
        public delegate bool IsPrimeSlowDelegate(int number, CancellationToken token);

        private static bool IsPrimeSlow(int number, CancellationToken token)
        {
            if (number <= 0) throw new Exception("输入必须大于0");
            if (number == 1) throw new Exception("1不是质数也不是合数");

            for (var i = 2; i < number; i++)
            {
                token.ThrowIfCancellationRequested();
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

            try
            {
                //获得结果，这里不会阻塞
                Console.WriteLine(ba.EndInvoke(iar));
                Console.WriteLine("任务成功完成！");
            }
            catch (OperationCanceledException ex)
            {
                //捕捉到异常
                Console.WriteLine("任务取消！");
            }
            catch(Exception ex)
            {
                //其他异常，来自代码，例如传入了0或1
                Console.WriteLine("其他异常");
            }
        }
    }
}
