using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapturedVariable
{
    class Program
    {
        static void Main(string[] args)
        {
            //匿名方法外部的变量
            var i = 9;
            var j = 10;

            //捕获了i
            var func = new Func<int>(delegate
            {
                Console.WriteLine("执行了匿名方法");

                //匿名函数捕获一个外部变量i
                return i + 1;
            });

            i = func();

            Console.WriteLine(i);  //10
            Console.ReadKey();
        }
    }
}
