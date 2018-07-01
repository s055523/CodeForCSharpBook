using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodTableLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new ExampleRef();
            Console.WriteLine("调用前");
            Console.ReadKey();
            Console.WriteLine("调用后");
            a.NormalMethod();
            Console.ReadKey();
        }

    }

    class ExampleRef
    {
        private int a = 1;
        public string b = "test";
        private static string c = "static";
        public virtual void VirtualMethod()
        {

        }
        public void NormalMethod()
        {

        }
        public static void StaticMethod()
        {

        }
    }

}
