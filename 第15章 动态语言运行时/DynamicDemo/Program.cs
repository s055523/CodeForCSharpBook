using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var constructor = typeof(TestClass).GetConstructors()[0];

            //通过反射调用构造函数
            var instance = constructor.Invoke(null);

            //通过反射调用方法需要将所有参数打包成一个object
            var method = typeof(TestClass).GetMethod("Add");
            method.Invoke(instance, new object[] { 1, 2 });

            //使用动态类型，创建对象只需要一步
            dynamic obj = Activator.CreateInstance(typeof(TestClass));

            //使用动态类型调用有参方法，就和普通方式一样
            obj.Add(1, 2);

            Console.ReadKey();
        }
    }

    class TestClass
    {
        public TestClass()
        {
            Console.WriteLine("调用构造函数");
        }

        public void Method()
        {
            Console.WriteLine("调用方法");
        }

        public void Add(int a, int b)
        {
            Console.WriteLine(a + b);
        }
    }

}
