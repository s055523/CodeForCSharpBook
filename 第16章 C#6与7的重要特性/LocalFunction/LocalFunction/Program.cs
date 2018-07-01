using static System.Console;
using System;

namespace LocalFunction
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine(Fibonacci(10));
            ReadKey();
        }

        public static void Foo()
        {
            string a = "123";
            Bar();

            void Bar()
            {
                WriteLine(a);
            }
        }
        public static void A()
        {
            string a = "123";
            Action b = () =>
            {
                WriteLine(a);
            };
            b();
        }

        public static int Fibonacci(int x)
        {
            if (x < 0) throw new ArgumentException("请输入正整数", nameof(x));
            return Fib(x).current;

            (int current, int previous) Fib(int i)
            {
                if (i == 1) return (1, 0);
                var (p, pp) = Fib(i - 1);
                return (p + pp, p);
            }
        }
    }
}
