using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Awaitable
{
    public interface IAwaiter : INotifyCompletion // 或者ICriticalNotifyCompletion
    {
        bool IsCompleted { get; }
        void GetResult();
    }

    public interface IAwaiter<out TResult> : INotifyCompletion // 或者ICriticalNotifyCompletion
    {
        bool IsCompleted { get; }
        TResult GetResult();
    }

    //继承IAwaiter<TResult>
    public struct FuncAwaiter<TResult> : IAwaiter<TResult>
    {
        private readonly Task<TResult> task;

        //在构造函数中启动任务
        public FuncAwaiter(Func<TResult> function)
        {
            task = new Task<TResult>(function);
            task.Start();
        }

        //IsCompleted属性的实现：借用任务的IsCompleted
        bool IAwaiter<TResult>.IsCompleted
        {
            get
            {
                return task.IsCompleted;
            }
        }

        //返回任务的结果，它的类型是TResult
        TResult IAwaiter<TResult>.GetResult()
        {
            return task.Result;
        }

        //在任务结束之后，启动它的接续任务
        void INotifyCompletion.OnCompleted(Action continuation)
        {
            if (task.IsCompleted)
            {
                new Task(continuation).Start();
            }
        }
    }
}
