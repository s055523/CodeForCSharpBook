using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceDispatch
{
    class Program
    {
        static void Method(IA a)


        {


            a.Foo();


        }

        static void Main(string[] args)
        {
            Demo1();
            Console.ReadKey();
        }

        static void Demo1()
        {
            Derived2 d1 = new Derived2();
            Derived d2 = d1;
            Base d3 = d1;
            ICommon d4 = d1;

            d1.Do(); //e,Derived2拥有Do方法
            d2.Do(); //e,方法被子类重写
            d3.Do(); //b,方法只是被隐藏，因此根据编译时类型选择父类方法
            d4.Do(); //c,方法被子类base重写，然后又被derived重写

            Console.ReadKey();
        }
    }
    

    interface IA


    {


        void Foo();


    }





    class A : IA


    {


        public void Foo()


        {
            Console.WriteLine("a");

        }


    }





    class B : IA


    {


        void IA.Foo()


        {
            Console.WriteLine("b");

        }


    }

    public interface ICommon
    {
        void Do();
    }
    public class Base : ICommon
    {
        void ICommon.Do()
        {
            Console.WriteLine("a");
        }
        public virtual void Do()
        {
            Console.WriteLine("b");
        }
    }
    public class Derived : Base, ICommon
    {
        void ICommon.Do()
        {
            Console.WriteLine("c");
        }
        public virtual void Do()
        {
            Console.WriteLine("d");
        }
    }
    public class Derived2 : Derived
    {
        public override void Do()
        {
            Console.WriteLine("e");
        }
    }
}
