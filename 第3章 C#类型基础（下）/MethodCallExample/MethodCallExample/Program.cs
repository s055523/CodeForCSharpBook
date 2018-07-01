using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodCallExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Father f1 = new Son();
            f1.AbstractMethod();
            f1.NormalMethod();
            f1.VirtualMethod();
            f1.FatherMethod();

            //f1无法呼叫到SonMethod，要靠IL呼叫

            Father f2 = null;

            //这样写会发生运行时错误
            //f2.FatherMethod();
            //尝试使用IL来呼叫方法


            Console.ReadKey();
        }
    }

    public abstract class AbstractClass
    {
        //抽象方法不能有主体，且必须位于抽象类中，子类必须实现
        //C# abstract = IL newslot abstract virtual
        public abstract void AbstractMethod();
    }

    public class Father : AbstractClass
    {
        //重写基类的抽象方法，子类仍然可以继续重写它
        //C# override = IL virtual
        public override void AbstractMethod()
        {
            Console.WriteLine("AbstractMethod实现");
        }

        //普通方法
        public void NormalMethod()
        {
            Console.WriteLine("NormalMethod");
        }

        //虚方法
        //C# virtual = IL newslot virtual
        public virtual void VirtualMethod()
        {
            Console.WriteLine("VirtualMethod");
        }

        public void FatherMethod()
        {
            Console.WriteLine("FatherMethod");
        }
    }

    public class Son : Father
    {
        //override sealed关键字合用，重写基类的虚方法/抽象方法，并且阻止子类进一步重写
        //C# sealed = IL final
        public override sealed void VirtualMethod()
        {
            Console.WriteLine("VirtualMethod - Son");
        }

        //隐藏父类的同签名方法
        //在IL中，new关键字并不做任何特殊处理
        public new void NormalMethod()
        {
            Console.WriteLine("NormalMethod - Son");
        }

        public override void AbstractMethod()
        {
            Console.WriteLine("AbstractMethod实现 - Son");
        }

        public void SonMethod()
        {
            Console.WriteLine("SonMethod");
        }
    }

    public class Test
    {
        public int i;
        public void Method(int a)
        {
            Method2(a);
            Method3(a);
            Method4(a);
            Method2(a);
        }
        public int Method2(int a)
        {
            return a + 2;
        }
        public int Method3(int a)
        {
            return a + 3;
        }
        public int Method4(int a)
        {
            return a + 4;
        }
    }
}
