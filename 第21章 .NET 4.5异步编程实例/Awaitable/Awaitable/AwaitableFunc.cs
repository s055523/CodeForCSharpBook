using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awaitable
{
    public class AwaitableFunc<T> : IAwaitable<T>
    {
        public IAwaiter<T> GetAwaiter()
        {
            throw new NotImplementedException();
        }
    }

    //令FuncAwaitable<TResult>继承IAwaitable<TResult>
    //方法的再包装
    public struct FuncAwaitable<TResult> : IAwaitable<TResult>
    {
        //方法本身
        private readonly Func<TResult> function;

        public FuncAwaitable(Func<TResult> function)
        {
            this.function = function;
        }

        public IAwaiter<TResult> GetAwaiter()
        {
            Console.WriteLine("GetAwaiter");
            return new FuncAwaiter<TResult>(function);
        }
    }
}
