using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CapturedVariable2
{
    class Program
    {
        static void Main(string[] args)
        {
            var func = CreateFunc();

            //当方法执行完之后，没有被捕获的局部变量生存期结束
            GC.Collect();

            Thread.Sleep(1000);

            //因为func还活着，所以它引用的所有捕获变量都活着，所以a对象不会死
            Console.WriteLine(func());

            func = null;

            //func死了之后a也会跟着死
            GC.Collect();

            Console.ReadKey();
        }
        public static Func<int> CreateFunc()
        {
            //方法的局部变量，被捕获
            var a = new DemoClass(1);

            //方法的局部变量，没有被捕获
            var b = new DemoClass(2);

            //创建一个泛型委托并返回
            var func = new Func<int>(delegate
            {
                Console.WriteLine("执行了匿名函数");
                return a.a;
            });
            return func;
        }

    }

    class DemoClass
    {
        public int a { get; set; }

        public DemoClass(int a)
        {
            this.a = a;
        }

        ~DemoClass()
        {
            Console.WriteLine(this.a + ":生存期结束");
        }
    }

}
