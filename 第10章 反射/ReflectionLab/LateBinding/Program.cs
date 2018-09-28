using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LateBinding
{
    class Program
    {
        static void Main(string[] args)
        {
            //获得目标程序集
            var assembly = Assembly.Load(@"D:\ReflectionLab.exe"); 

            //获得目标对象
            var type = assembly.GetType("Reflection.ReflectionTest");

            //如果不加入BindingFlags变量，无法获得私有构造函数
            var constructor = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

            //通过反射，在外界照样可使用私有构造函数
            object obj = constructor[0].Invoke(null);

            //获得所有实例成员（公开和非公开的），不包括父类
            var finfos = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var f in finfos)
            {
                Console.WriteLine("【" + f.MemberType + "】：" + f.Name);

                if (f.Name == "b")
                {
                    f.SetValue(obj, 2);
                }
                Console.WriteLine("字段名称：{0}, 字段类型:{1}, 值为:{2}", f.Name, f.FieldType, f.GetValue(obj));
            }

            //如果不加入BindingFlags.NonPublic变量，无法获得私有方法
            var method = type.GetMethod("AddOne", BindingFlags.Instance | BindingFlags.NonPublic);
            object[] parameter = { 1 };

            //调用了AddOne，打印出2
            Console.WriteLine(method.Invoke(obj, parameter));

            Console.ReadKey();

        }
    }
}
