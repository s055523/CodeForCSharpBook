using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallVSCallvirt
{
    class Program
    {
        static void Main(string[] args)
        {
            int i =1;
            C c1 = new C();
            B c2 = new C();
            A c3 = new C();

            //
            c1.Foo();
            c2.Foo();
            c3.Foo();

            Console.ReadKey();
        }
    }

    //方法表：object方法加上A.Foo和A的构造函数
    class A
    {
        public virtual void Foo()
        {
            Console.WriteLine("A.Foo");
        }
    }
    //方法表：object方法加上A.Foo,B.Foo和B的构造函数
    class B : A
    {
        //隐藏了基类的Foo
        //virtual = IL virtual + newslot，所以方法表需要增加一个插槽
        public virtual void Foo()
        {
            Console.WriteLine("B.Foo");
        }
    }
    //方法表：object方法加上A.Foo,C.Foo和C的构造函数，它代替了B.Foo
    class C : B
    {
        //override = IL virtual，所以方法表不需要增加插槽，之前的方法被该方法取代
        public override void Foo()
        {
            Console.WriteLine("C.Foo");
        }
    }
}
