using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldStateMachine
{
    class Program
    {
        public static void Main(string[] args)
        {
            GetEnumerableTest();
            Console.ReadKey();
        }

        static IEnumerable<string> GetEnumerableTest()
        {
            yield return "begin";
            Console.WriteLine("begin");

            for (int i = 0; i < 10; i++)
            {
                yield return i.ToString();
                Console.WriteLine(i);
            }

            yield return "end";
            Console.WriteLine("end");
        }
    }
}
