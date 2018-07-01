using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicGeneric
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> l = new List<string> { ":", "drg", "dthtj" };
            Console.WriteLine(l.DynamicSum()); //:drgdthtj

            List<double> d = new List<double> { 0.1, 0.2, 0.3 };
            Console.WriteLine(d.DynamicSum()); //0.6

            Console.ReadKey();
        }
    }

    public static class ListExtension
    {
        public static T DynamicSum<T>(this IEnumerable<T> data)
        {
            dynamic sum = default(T);
            foreach (dynamic d in data)
            {
                //需要保证这里的相加是合法的，因为运行时才会报错
                sum = (T)sum + d;
            }
            return sum;
        }

        public static T GenericSum<T>(this IEnumerable<T> data)
        {
            T sum = default(T);
            foreach (var d in data)
            {
                //编译时报错
                //sum = (T)sum + d;
            }
            return sum;
        }
    }
}
