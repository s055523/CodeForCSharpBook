using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LazyLab
{
    class Program
    {
        static void Main(string[] args)
        {
            //普通方式建立对象
            var immediately = new ExpensiveClass(1000);

            //使用延迟初始化建立对象，需要多包一层
            var ec = new Lazy<ExpensiveClass>(() => new ExpensiveClass(999));

            Console.WriteLine("延迟初始化对象ec建立了吗：" + ec.IsValueCreated);

            //访问ec.Value是触发延迟初始化对象建立的方式，在打印a之前，会先调用构造函数
            Console.WriteLine(ec.Value.a);

            ExpensiveClass ec2 = null;

            //如果不想将对象包装在Lazy<T>并每次都按照Value的方式访问，但还想延迟初始化
            //则可以使用LazyInitializer.EnsureInitialized类，在需要的时候才初始化
            LazyInitializer.EnsureInitialized(ref ec2, () => new ExpensiveClass(999));

            Console.ReadKey();
        }
    }

    class ExpensiveClass
    {
        public int a;
        public ExpensiveClass(int a)
        {
            this.a = a;
            Console.WriteLine("建立a的值为{0}的实例", a);
        }
    }

}
