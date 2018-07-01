using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Deconstructor
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new People();
            p.FirstName = "Test";

            //解构出姓名，因为不是元组，所以必需有手动写好的解构方法才能运行
            var (fName, mName, lName) = p;
            WriteLine(fName);

            var d = DateTime.Now;
            (string s, DayOfWeek dow) = d;

            WriteLine($"今天是{s}, 是{d}");

            ReadKey();
        }

        void Demo1()
        {
            //定义元组
            (int count, double sum, double sumOfSquares) tuple = (1, 2, 3);

            //使用方差的计算公式得到方差
            var variance = tuple.sumOfSquares - tuple.sum * tuple.sum / tuple.count;

            //将一个元组放在等号右边，将对应的变量值和类型放在等号左边，就会导致解构
            //变量可以用隐式类型
            (int count, double sum, double sumOfSquares) = (1, 2, 3);

            //解构之后的方差计算，代码简洁美观
            variance = sumOfSquares - sum * sum / count;

            //也可以这样解构，这将会导致编译器推断元组的类型为三个int
            var (a, b, c) = (1, 2, 3);

            ReadKey();
        }
    }

    //示例类型
    public class People
    {
        public int ID;
        public string FirstName;
        public string MiddleName;
        public string LastName;
        public int Age;
        public string CompanyName;

        //解构全名
        public void Deconstruct(out string f, out string m, out string l)
        {
            f = FirstName;
            m = MiddleName;
            l = LastName;
        }
    }

    public static class ReflectionExtensions
    {
        //解构DateTime并获得想要的值
        public static void Deconstruct(this DateTime dateTime, out string DateString, out DayOfWeek dayOfWeek)
        {
            DateString = dateTime.ToString("yyyy-MM-dd");
            dayOfWeek = dateTime.DayOfWeek;
        }
    }
}
