using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEnumerable1._1._5
{
    class Program
    {
        public static void Main(string[] args)
        {
            var t = GetEnumerableTest();
            foreach (var member in t)
            {
                //member是只读的，但它的成员却不是
                //不能修改它的值，但可以修改它的字段的值
                //member = new Example(2,3);
                member.a = 99999;
            }

            foreach (var member in t)
            {
                Console.WriteLine(member);
            }

            Console.ReadKey();
        }

        static IEnumerable<Example> GetEnumerableTest()
        {
            for (int i = 0; i < 3; i++)
            {
                yield return new Example(i, i);
            }
        }
    }

    public class Example
    {
        private int _a;

        public int a
        {
            get { return _a; }
            set
            {
                _a = value;
                Console.WriteLine("a = " + value);
            }
        }

        public int b { get; set; }

        public Example(int x, int y)
        {
            Console.WriteLine("被创建了");
            _a = x;
            b = y;
        }

        public override string ToString()
        {
            return string.Format("a = {0}, b = {1}", a, b);
        }
    }

}
