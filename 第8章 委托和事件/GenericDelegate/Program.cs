using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDelegate
{
    class GenericDelegate
    {
        //这些代码不需要了
        //public delegate void MyAction<T>(T arg1, T arg2);
        //public delegate T MyFunc<T>(T arg1, T arg2);

        static void Main(string[] args)
        {
            //var myaction = new MyAction<int>(add);
            var a = new Action<int, int>(add);
            a(1, 2);

            //Func委托的最后一个参数是返回值的类型
            //var myfunc = new MyFunc<int>(add2);
            var b = new Func<int, int, int>(add2);

            Console.WriteLine(b(1, 2));
            Console.ReadKey();
        }

        //这个EventHandler不返回值
        public static void add(int a, int b)
        {
            Console.WriteLine(a + b);
        }

        //这个EventHandler返回一个整数
        public static int add2(int a, int b)
        {
            return a + b;
        }
    }

}
