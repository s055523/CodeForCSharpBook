using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contravariance
{
    class Program
    {
        public static void Main(string[] args)
        {
            Action<Parent> parentAction = Print;

            //Action<T>支持逆变
            //将Action<Parent>转换为Action<Child>
            Action<Child> childAction = parentAction;
            childAction(new Child());
        }

        public static void Print(Parent p)
        {
            Console.WriteLine("Parent");
        }

        public class Parent { }
        public class Child : Parent { }

    }
}
