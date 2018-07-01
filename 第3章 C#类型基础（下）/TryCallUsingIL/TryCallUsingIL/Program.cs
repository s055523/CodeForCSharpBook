using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryCallUsingIL
{
    class Program
    {
        static void Main(string[] args)
        {
            A a = new B();
            Console.WriteLine(a.GetType());   //B

            A a2 = (A)new B();
            Console.WriteLine(a2.GetType());  //B

            a.Method();
        }
    }

    public class A
    {
        public int a;
        public void Method()
        {
            Console.WriteLine("A的方法");
        }
    }

    public class B : A
    {
        public int b;
        public void MethodB()
        {
            Console.WriteLine("B的方法");
        }
    }

}
