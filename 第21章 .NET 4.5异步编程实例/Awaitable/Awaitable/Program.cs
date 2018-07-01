using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Awaitable
{
    class Program
    {
        static void Main(string[] args)
        {
            DoAsync();
            Console.ReadKey();
        }

        private static async void DoAsync()
        {
            FuncAwaitable<int> func = new FuncAwaitable<int>(() =>
            {
                Console.WriteLine("运行任务");
                return 0;
            });

            //可以等待lambda表达式了
            //这等同于func.GetAwaiter()
            int result = await func;           
        }
    }
}
