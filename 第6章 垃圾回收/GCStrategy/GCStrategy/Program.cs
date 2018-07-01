using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GCStrategy
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public sealed class SimpleClass : IDisposable
    {
        private string a;
        private List<string> b;

        public SimpleClass(string a)
        {
            this.a = a;
            this.b = new List<string>();
        }

        //其他方法

        //这个Dispose毫无意义
        public void Dispose()
        {
            if (this.b != null)
            {
                this.b.Clear();
                this.b = null;
            }
        }
    }

    public sealed class SimpleClassWithIDisposableMember : IDisposable
    {
        public string a;
        public List<string> b;
        public Mutex m;

        public SimpleClassWithIDisposableMember(string a)
        {
            this.a = a;
            this.b = new List<string>();
            m = new Mutex();
        }

        //其他方法

        //呼叫所有成员的Dispose方法（有时被Close再包装了）
        public void Dispose()
        {
            if (m != null)
            {
                m.Dispose();
            }
        }
    }

    public sealed class HardClass : IDisposable
    {
        //非托管资源
        private IntPtr fileHandle;

        //实现IDisposable接口的托管资源
        private Mutex mutex;

        public HardClass(IntPtr handle, Mutex m)
        {
            fileHandle = handle;
            mutex = m;
        }

        //私有方法清理托管和非托管资源
        //如果该类是密封类，使用private，否则使用protected virtual，这样子类可以复写它，加入更多的逻辑
        private void Dispose(bool IsDisposed)
        {
            // Do nothing if the handle is invalid
            if (IsDisposed)
            {
                //清理托管资源（被析构函数呼叫时不需要清理托管资源）
                if (mutex != null) mutex.Dispose();
            }

            //清理非托管资源
            if (fileHandle != IntPtr.Zero)
            {
                CloseHandle(fileHandle);
                fileHandle = IntPtr.Zero;
            }
        }

        //实现Dispose方法供外界使用，呼叫私有方法，之后呼叫SuppressFinalize。
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~HardClass()
        {
            //实现一个解构函数（这会覆盖原有的Finalize方法）在其中呼叫私有方法。
            //这是为了防止用户忘了呼叫Dispose方法而最终没有回收非托管资源。
            //原有的Finalize方法并不会理会非托管资源。
            if (fileHandle != IntPtr.Zero)
            {
                //传入false以清理非托管资源
                Dispose(false);
            }
        }

        [System.Runtime.InteropServices.DllImport("Kernel32")]
        private extern static bool CloseHandle(IntPtr handle);
    }
}
