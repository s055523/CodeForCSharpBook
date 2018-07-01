using System;

namespace MethodTableWithInterface
{
    interface ITest
    {
        string interfaceMethod();
    }

    class FatherClass : ITest
    {
        public static int i = 1;
        public string interfaceMethod()
        {
            Console.WriteLine("继承接口的方法");
            return "test";
        }

        public int NormalMethod(int a)
        {
            return a + 1;
        }
        public int NormalMethod2(int a)
        {
            return a + 2;
        }
        public int NormalMethod3(int a)
        {
            return a + 3;
        }

        public virtual void VirtualMethod1()
        {
            Console.WriteLine("VirtualMethod1");
        }
        public virtual void VirtualMethod2()
        {
            Console.WriteLine("VirtualMethod2");
        }
    }
}
