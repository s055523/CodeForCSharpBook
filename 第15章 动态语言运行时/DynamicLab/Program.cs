using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DynamicLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var student = new Student { ID = 1, Name = "Dynamic" };
            object d = student;

            //不能再直接.ID取得字段了，但是，其实d是和student指向同一个Student的实例的，所以d存在.ID
            //但是不依靠DLR，直接访问是不可能访问到的
            //Console.WriteLine(d.ID);

            //初始化调用点1
            if (Sites.site1 == null)
            {
                //这个调用点会绑定WriteLine方法
                Sites.site1 = CallSite<Action<CallSite, Type, object>>.Create(
                    Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(
                    CSharpBinderFlags.ResultDiscarded,
                    "WriteLine",
                    null,
                    typeof(Program),
                    new CSharpArgumentInfo[]
                    {
                        CSharpArgumentInfo.Create(
                            CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
                        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                    }));
            }
            //初始化调用点2
            if (Sites.site2 == null)
            {
                Sites.site2 = CallSite<Func<CallSite, object, object>>.Create(
                    Microsoft.CSharp.RuntimeBinder.Binder.GetMember(
                    CSharpBinderFlags.None, "ID", typeof(Program), new CSharpArgumentInfo[]
                    {
                        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                    }));
            }

            //方法调用I：传入调用点，和object类型，返回一个object类型
            //这里传入的类型就是我们建立的d，它是object类型，没有属性，但我们使用DLR便可以获得它的"属性"
            var obj = Sites.site2.Target(Sites.site2, d);

            //方法调用II：传入调用点，类型对象和第二个调用点返回的类型，呼叫WriteLine方法
            Sites.site1.Target(Sites.site1, typeof(Console), obj);

            Demo();

            Console.ReadKey();
        }

        //模拟编译器自动生成的调用点
        static class Sites
        {
            //对应于Console.WriteLine，这是一个不需要返回值的方法
            public static CallSite<Action<CallSite, Type, object>> site1;

            //对应于取Name
            public static CallSite<Func<CallSite, object, object>> site2;

            public static CallSite<Func<CallSite, object, int, object>> site3;
            public static CallSite<Func<CallSite, object, int, object>> site4;
        }

        public static void Demo()
        {
            object a = 1;

            //a = a + 5的调用点
            if (Sites.site3 == null)
            {
                Sites.site3 = CallSite<Func<CallSite, object, int, object>>.Create(
                    Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Add, typeof(Program), new CSharpArgumentInfo[]
                    {
                        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
                    }));
            }
            a = Sites.site3.Target(Sites.site3, a, 5);

            //规则：检查a是不是可以和5进行加法运算
            var rules3 = Sites.site3.GetType()
                .GetField("Rules", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(Sites.site3) as Func<CallSite, object, int, object>[];

            Console.WriteLine(rules3[0].GetHashCode());

            //a = a + 8的调用点
            if (Sites.site4 == null)
            {
                Sites.site4 = CallSite<Func<CallSite, object, int, object>>.Create(
                    Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Add, typeof(Program), new CSharpArgumentInfo[]
                    {
                        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
                    }));
            }
            a = Sites.site4.Target(Sites.site4, a, 8);

            //使用和site3相同的规则
            var rules4 = Sites.site4.GetType()
                .GetField("Rules", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(Sites.site4) as Func<CallSite, object, int, object>[];

            Console.WriteLine(rules4[0].GetHashCode());
        }
    }

    class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
