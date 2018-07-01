using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadAsyncCancellation
{
    class CancelHelper
    {
        readonly object _cancelLocker = new object();

        //布尔值
        bool _cancelRequest;

        //线程安全的查看布尔值
        public bool IsCancellationRequested
        {
            get { lock (_cancelLocker) return _cancelRequest; }
        }

        //供外界呼叫，线程安全的将布尔值设置为真
        public void Cancel() { lock (_cancelLocker) _cancelRequest = true; }

        //取消时抛出异常
        //OperationCanceledException是合适的类型
        public void ThrowIfCancellationRequested()
        {
            if (IsCancellationRequested) throw new OperationCanceledException();
        }
    }

}
