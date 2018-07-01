using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodTableWithInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new FatherClass();
            Console.WriteLine("没调用方法");
            Console.ReadKey();

            a.NormalMethod(100);
            Console.WriteLine("调用方法");
            Console.ReadKey();
        }
    }
}
