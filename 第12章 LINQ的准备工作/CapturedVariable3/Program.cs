using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapturedVariable3
{
    class Program
    {
        static void Main(string[] args)
        {
            var func = CreateFunc2();

            //因为func还活着，所以它引用的所有捕获变量都活着，所以num对象不会死
            //但是num是值类型，它如果在栈上的话，应该已经被销毁了
            //所以它实际上已经不在栈上了！
            Console.WriteLine(func());
            Console.ReadKey();
        }

        public static Func<string> CreateFunc2()
        {
            var str = "匿名函数的输出是";

            //捕获值类型
            //捕获值类型
            var num = 999;
            var num2 = 1;
            var func = new Func<string>(delegate
            {
                return str + num + "和" + num2;
            });
            return func;
        }

    }
}
