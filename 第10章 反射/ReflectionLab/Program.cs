using System;
using System.Collections;
using System.Reflection;

namespace ReflectionLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var type = typeof(ReflectionTest);
            Console.WriteLine("命名空间名称：" + type.Namespace);
            Console.WriteLine("直接基类型：" + type.BaseType);
            Console.WriteLine("全名：" + type.FullName);
            Console.WriteLine("是抽象类型：" + type.IsAbstract);
            Console.WriteLine("是类：" + type.IsClass);

            var type2 = typeof(IEnumerable);
            Console.WriteLine("是接口：" + type2.IsInterface);
            Console.WriteLine("直接基类型：" + type2.BaseType);

            var members = type.GetMembers(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var m in members)
            {
                Console.WriteLine("【" + m.MemberType + "】：" + m.Name);
            }

            Console.ReadKey();
        }
    }

    internal class ReflectionTest
    {
        //字段
        static int a;
        private int b;
        protected int aa;

        //属性
        public double c { get; set; }
        public static decimal d { get; set; }

        //方法
        ReflectionTest()
        {
            Console.WriteLine("调用构造函数");
        }

        public string AMethod()
        {
            return "返回一个字符串";
        }
    }
}
