using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStatic
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Generic<int>();
            Generic<int>.a = 1;

            var b = new Generic<string>();
            Generic<string>.a = 2;

            //1
            Console.WriteLine(Generic<int>.a);

            //2
            Console.WriteLine(Generic<string>.a);

            Console.ReadKey();
        }
    }

    class Generic<T>
    {
        public static int a;
    }
}
