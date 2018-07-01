using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncBackground
{
    class Program
    {
        //调用方法
        public static void Main(string[] args)
        {
            UseAsync(0);
            Console.ReadKey();
        }

        //调用异步操作的异步方法
        static async Task UseAsync(int number)
        {
            //A区（方法开始到下一个await之前）
            Console.WriteLine("await之前的代码");

            await Task.Run(() => AsyncOperation(number));

            //B区（上一个await之后，到下一个await之前）
            Console.WriteLine("await1的后续操作代码");

            await Task.Run(() => AsyncOperation2(number));

            //C区（最后一个await之后到方法结束）
            Console.WriteLine("await2的后续操作代码");
        }

        private static void AsyncOperation(int number)
        {
            Console.WriteLine("异步操作" + number);
            Thread.Sleep(1000);
        }

        private static void AsyncOperation2(int number)
        {
            Console.WriteLine("异步操作2" + number);
            Thread.Sleep(1000);
        }
    }
}
