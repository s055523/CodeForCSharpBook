using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Closure
{
    public class Closure
    {
        public Func<int> T1()
        {
            var n = 999;
            return delegate
            {
                Console.WriteLine(n);
                return n;
            };
        }
    }

    class Program
    {
        static void Main()
        {
            var a = new Closure();
            var b = a.T1();
            Console.WriteLine(b());

            Console.ReadKey();
        }
    }
}
