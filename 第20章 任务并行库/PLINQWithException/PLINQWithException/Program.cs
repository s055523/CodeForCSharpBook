using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLINQWithException
{
    class Program
    {
        static void Main(string[] args)
        {
            //产生一个带有0的数列，从-5到4
            var data = Enumerable.Range(-5, 10).ToArray();

            try
            {
                //使用100去除这些数，会抛出除零异常
                var a = data.AsParallel().Select(Divide);

                //强制PLINQ执行
                Console.WriteLine(a.Count());
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine("已经被取消");
            }
            catch (AggregateException ex)
            {
                foreach (var innerException in ex.Flatten().InnerExceptions)
                {
                    //会在这里捕捉到除零异常
                    Console.WriteLine(innerException.Message);
                }
            }

            Console.ReadKey();
        }

        private static int Divide(int number)
        {
            //有机会抛出除零异常
            return 100 / number;
        }

    }
}
