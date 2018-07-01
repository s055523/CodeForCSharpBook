using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReferenceCounting
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo2();
        }

        static void Demo2()
        {
            var obj1 = new A(); //这个对象x有一个引用
            var obj2 = new B(); //这个对象y有一个引用
            obj1.b = obj2;
            obj2.a = obj1;
            obj1 = null;
            obj2 = null;

            GC.Collect(0);
            Console.ReadKey();
        }

        static void Demo1()
        {
            var obj1 = new A(); //x有一个引用
            var obj2 = new A(); //y有一个引用
            var obj3 = obj1;    //x有两个引用

            obj1 = null;
            obj2 = null;

            //现在，x还剩一个引用，y没有引用了
            GC.Collect(0);       //y被回收

            Thread.Sleep(3000); //等待GC工作完成，否则，可能会先打印下面的------
            Console.WriteLine("------");

            obj3 = null;
            GC.Collect(0);

            Thread.Sleep(3000);
            Console.WriteLine("不在这里，升代了！");

            GC.Collect(1);      //x被回收
            Console.ReadKey();
        }
    }
    class A
    {
        public B b;
        ~A()
        {
            Console.WriteLine("A被回收了");
        }
    }
    class B
    {
        public A a;
        ~B()
        {
            Console.WriteLine("B被回收了");
        }
    }

}
