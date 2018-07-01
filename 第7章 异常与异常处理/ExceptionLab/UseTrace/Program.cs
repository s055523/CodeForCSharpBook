using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseTrace
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Clear();  //清除系统的默认监听器
            Trace.Listeners.Add(new MyListener()); //添加MyTraceListener实例

            try
            {
                Console.WriteLine("try");

                //激发一个FormatException
                var a = Convert.ToInt32("hello");
            }
            catch (FormatException ex)
            {
                Trace.TraceWarning("出现了一个FormatException");
                Trace.TraceInformation("Message: " + ex.Message);
                Trace.TraceInformation("Stacktrace: " + ex.StackTrace);

                Console.WriteLine("Catch");
            }
            Console.ReadKey();
        }
    }

    public class MyListener : TraceListener
    {
        public override void Write(string message)
        {
            File.AppendAllText("d:\\1.log", message);
        }

        public override void WriteLine(string message)
        {
            File.AppendAllText("d:\\1.log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss    ") + message + Environment.NewLine);
        }
    }
}
