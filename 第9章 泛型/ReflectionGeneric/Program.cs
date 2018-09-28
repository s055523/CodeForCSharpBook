using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionGeneric
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var type = typeof(AClass);

            var methods = type.GetMethods();

            foreach (var methodInfo in methods)
            {
                Console.WriteLine(methodInfo.Name);
                if (methodInfo.Name == "Print")
                {
                    //如果不存在泛型参数
                    if (!methodInfo.ContainsGenericParameters)
                    {
                        methodInfo.Invoke(new AClass(), new object[] { "123" });
                    }
                    else
                    {
                        //使用MakeGenericMethod建立泛型方法
                        var genericMethod = methodInfo.MakeGenericMethod(typeof(string));

                        //此时就有了一个和Print(string s)签名相同的方法，可以调用了
                        genericMethod.Invoke(new AClass(), new object[] { "123" });
                    }
                }
            }

            Console.ReadKey();
        }
    }

    public class AClass
    {
        public void Print(string s)
        {
            Console.WriteLine("这是非泛型方法");
        }
        public void Print<T>(T t)
        {
            Console.WriteLine(t.GetType());
        }
    }
}
