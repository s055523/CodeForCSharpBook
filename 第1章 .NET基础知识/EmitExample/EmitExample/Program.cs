using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmitExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建程序集
            EmitLab();

            //加载动态程序集
            var asm = Assembly.Load("HelloWorldAssembly");

            //获得HelloWorld类
            var type = asm.GetType("HelloWorld");

            //获得Print方法
            var m = type.GetMethod("Print");

            //创建一个HelloWorld类的实例，并调用方法
            var instance = Activator.CreateInstance(type);
            m.Invoke(instance, null);

            Console.ReadKey();
        }

        static void EmitLab()
        {
            //得到当前线程所在的应用程序域
            var appdomain = Thread.GetDomain();

            //必须在应用程序域中才能创建程序集
            var assemblyName = new AssemblyName
            {
                Name = "HelloWorldAssembly"
            };
            var assembly = appdomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Save);

            //在程序集中定义模块，如果程序集只有一个模块，可以将程序集名设置为主模块的名字
            var module = assembly.DefineDynamicModule("MainModule", "HelloWorldAssembly.dll");

            //定义一个公共类
            var classHelloWorld = module.DefineType("HelloWorld", TypeAttributes.Public);

            //定义一个名为Print的方法，没有参数，什么也不返回
            var method = classHelloWorld.DefineMethod("Print", MethodAttributes.Public, null, null);
            var methodIL = method.GetILGenerator();

            //为方法加入输出
            methodIL.EmitWriteLine("Hello world!");

            //这里调用了助记符ret返回
            methodIL.Emit(OpCodes.Ret);

            //在做好所有工作之后，创建HelloWorld类
            classHelloWorld.CreateType();

            //保存程序集
            assembly.Save("HelloWorldAssembly.dll");
        }

    }
}
