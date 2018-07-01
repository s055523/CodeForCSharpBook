using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callvirt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("hello, world");
        }
    }

    class A
    {
        public void Print()
        {
            Console.WriteLine("A");
        }
    }

    class B : A
    {
        public void Print()
        {
            Console.WriteLine("B");
        }
    }

}
