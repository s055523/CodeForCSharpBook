using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructorLab
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo1();
            Console.ReadKey();
        }

        static void Demo1()
        {
            var b = new Father();
            Console.WriteLine("---");
            var a = new Son();
        }

        static void Demo2()
        {
            Console.WriteLine("开始");
            Foo.GetString("输入");
        }

        static void Demo3()
        {
            Console.WriteLine("开始");
            Foo.GetString("输入");
            string f = Foo.s;
        }
    }

    class Father
    {
        public static int i;
        static Father()
        {
            i = 1;
            Console.WriteLine("父类型的静态构造函数");
        }
        public Father()
        {
            Console.WriteLine("父类型的无参实例构造函数");
        }
        public Father(int input)
        {
            Console.WriteLine("父类型的有参实例构造函数");
        }
    }
    class Son : Father
    {
        public static int j;
        static Son()
        {
            j = 1;
            Console.WriteLine("子类型的静态构造函数");
        }
        public Son() : this(5)
        {
            Console.WriteLine("子类型的无参实例构造函数");
        }
        public Son(int input)
        {
            j = input;
            Console.WriteLine("子类型的有参实例构造函数");
        }
    }

    class Foo
    {
        public static string s = GetString("静态字段");
        public static string GetString(string s)
        {
            Console.WriteLine(s);
            return s;
        }
    }
}
